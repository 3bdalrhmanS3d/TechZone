using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechZone.Core.DTOs.Brand;
using TechZone.Core.Entities;
using TechZone.Core.PagedResult;
using TechZone.Core.Service.Interfaces;
using TechZone.Core.ServiceResponse;
using TechZone.EF.Application;

namespace TechZone.EF.Service.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<BrandService> _logger;
        private readonly Cloudinary? _cloudinary;

        public BrandService(ApplicationDbContext db, ILogger<BrandService> logger)
        {
            _db = db;
            _logger = logger;

            var cloudUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            if (!string.IsNullOrWhiteSpace(cloudUrl))
            {
                _cloudinary = new Cloudinary(cloudUrl);
                _cloudinary.Api.Secure = true;
            }
        }

        public async Task<ServiceResponse<PagedResult<BrandDto>>> GetAllAsync(PaginationParamsDto<BrandSortBy> request, CancellationToken ct = default)
        {
            try
            {
                var query = _db.Brands.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    string term = request.Search.Trim().ToLower();
                    query = query.Where(b =>
                        b.Name.ToLower().Contains(term) ||
                        (b.Country ?? string.Empty).ToLower().Contains(term)
                    );
                }

                bool desc = request.SortDirection == SortDirection.Desc;
                var sortBy = request.SortBy;

                query = sortBy switch
                {
                    BrandSortBy.Name => desc ? query.OrderByDescending(b => b.Name) : query.OrderBy(b => b.Name),
                    BrandSortBy.Country => desc ? query.OrderByDescending(b => b.Country) : query.OrderBy(b => b.Country),
                    _ => desc ? query.OrderByDescending(b => b.Id) : query.OrderBy(b => b.Id)
                };

                int total = await query.CountAsync(ct);
                int skip = (request.Page <= 1 ? 0 : (request.Page - 1) * request.PageSize);

                var items = await query.Skip(skip).Take(request.PageSize)
                    .Select(b => new BrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Country = b.Country ?? string.Empty,
                        LogoUrl = b.LogoUrl ?? string.Empty,
                        Description = b.Description ?? string.Empty
                    })
                    .ToListAsync(ct);

                var page = new PagedResult<BrandDto>(items, total, request.Page, request.PageSize);
                return ServiceResponse<PagedResult<BrandDto>>.SuccessResponse(page, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.GetAllAsync");
                return ServiceResponse<PagedResult<BrandDto>>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<BrandDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var dto = await _db.Brands.AsNoTracking()
                    .Where(b => b.Id == id)
                    .Select(b => new BrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Country = b.Country ?? string.Empty,
                        LogoUrl = b.LogoUrl ?? string.Empty,
                        Description = b.Description ?? string.Empty
                    })
                    .FirstOrDefaultAsync(ct);

                if (dto == null)
                    return ServiceResponse<BrandDto>.NotFoundResponse("Brand not found", "الماركة غير موجودة");

                return ServiceResponse<BrandDto>.SuccessResponse(dto, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.GetByIdAsync");
                return ServiceResponse<BrandDto>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<BrandDto>> CreateAsync(CreateBrandRequest dto, CancellationToken ct = default)
        {
            try
            {
                var name = (dto.Name ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(name))
                    return ServiceResponse<BrandDto>.ErrorResponse("Name is required", "الاسم مطلوب", 400);

                if (dto.Image is null || dto.Image.Length == 0)
                    return ServiceResponse<BrandDto>.ErrorResponse("Logo image is required", "صورة اللوجو مطلوبة", 400);

                if (!IsImage(dto.Image))
                    return ServiceResponse<BrandDto>.ErrorResponse("Only image files are allowed", "يُسمح بملفات الصور فقط", 400);

                bool exists = await _db.Brands.AnyAsync(b => b.Name.ToLower() == name.ToLower(), ct);
                if (exists)
                    return ServiceResponse<BrandDto>.ConflictResponse($"Brand with name '{name}' already exists.", "اسم الماركة مستخدم بالفعل");

                var logoUrl = await UploadBrandLogoAsync(dto.Image, ct);
                if (string.IsNullOrWhiteSpace(logoUrl))
                    return ServiceResponse<BrandDto>.ErrorResponse("Image upload failed", "فشل رفع الصورة", 500);

                var entity = new Brand
                {
                    Name = name,
                    Country = dto.Country?.Trim() ?? string.Empty,
                    LogoUrl = logoUrl,
                    Description = dto.Description?.Trim() ?? string.Empty
                };

                await _db.Brands.AddAsync(entity, ct);
                await _db.SaveChangesAsync(ct);

                var result = new BrandDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Country = entity.Country ?? string.Empty,
                    LogoUrl = entity.LogoUrl ?? string.Empty,
                    Description = entity.Description ?? string.Empty
                };

                var resp = ServiceResponse<BrandDto>.SuccessResponse(result, "Created", "تم الإنشاء");
                resp.StatusCode = 201;
                return resp;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in BrandService.CreateAsync");
                return ServiceResponse<BrandDto>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.CreateAsync");
                return ServiceResponse<BrandDto>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateBrandRequest dto, CancellationToken ct = default)
        {
            try
            {
                var entity = await _db.Brands.FirstOrDefaultAsync(b => b.Id == id, ct);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Brand not found", "الماركة غير موجودة");

                var newName = (dto.Name ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(newName))
                    return ServiceResponse<object>.ErrorResponse("Name is required", "الاسم مطلوب", 400);

                bool nameTaken = await _db.Brands.AnyAsync(b => b.Id != id && b.Name.ToLower() == newName.ToLower(), ct);
                if (nameTaken)
                    return ServiceResponse<object>.ConflictResponse($"Brand with name '{newName}' already exists.", "اسم الماركة مستخدم بالفعل");

                entity.Name = newName;
                entity.Country = dto.Country?.Trim() ?? string.Empty;
                entity.Description = dto.Description?.Trim() ?? string.Empty;

                if (dto.ClearLogo == true)
                {
                    entity.LogoUrl = string.Empty;
                }
                else if (dto.Image is not null && dto.Image.Length > 0)
                {
                    if (!IsImage(dto.Image))
                        return ServiceResponse<object>.ErrorResponse("Only image files are allowed", "يُسمح بملفات الصور فقط", 400);

                    var newUrl = await UploadBrandLogoAsync(dto.Image, ct);
                    if (string.IsNullOrWhiteSpace(newUrl))
                        return ServiceResponse<object>.ErrorResponse("Image upload failed", "فشل رفع الصورة", 500);

                    // (اختياري) لو بتخزن public_id احذف القديم من Cloudinary هنا
                    entity.LogoUrl = newUrl;
                }

                await _db.SaveChangesAsync(ct);
                return ServiceResponse<object>.SuccessResponse(null, "Updated", "تم التحديث");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in BrandService.UpdateAsync");
                return ServiceResponse<object>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.UpdateAsync");
                return ServiceResponse<object>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _db.Brands.FirstOrDefaultAsync(b => b.Id == id, ct);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Brand not found", "الماركة غير موجودة");

                bool inUse = await _db.Laptops.AnyAsync(l => l.BrandId == id, ct);
                if (inUse)
                    return ServiceResponse<object>.ConflictResponse("Cannot delete brand because it has related laptops.", "لا يمكن حذف الماركة لوجود لابتوبات مرتبطة بها");

                _db.Brands.Remove(entity);
                await _db.SaveChangesAsync(ct);

                return ServiceResponse<object>.SuccessResponse(null, "Deleted", "تم الحذف");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in BrandService.DeleteAsync");
                return ServiceResponse<object>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.DeleteAsync");
                return ServiceResponse<object>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<List<BrandWithCountDto>>> GetWithLaptopCountsAsync(CancellationToken ct = default)
        {
            try
            {
                var data = await _db.Brands.AsNoTracking()
                    .OrderBy(b => b.Name)
                    .Select(b => new BrandWithCountDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        LaptopsCount = b.Laptops.Count()
                    })
                    .ToListAsync(ct);

                return ServiceResponse<List<BrandWithCountDto>>.SuccessResponse(data, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BrandService.GetWithLaptopCountsAsync");
                return ServiceResponse<List<BrandWithCountDto>>.InternalServerErrorResponse();
            }
        }

        
        private static bool IsImage(IFormFile f)
        => f.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

        private async Task<string> UploadBrandLogoAsync(IFormFile file, CancellationToken ct)
        {
            if (_cloudinary is null) return string.Empty;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "techzone/brands"
            };
            var result = await _cloudinary.UploadAsync(uploadParams, ct);
            return result?.SecureUrl?.ToString() ?? string.Empty;
        }
    }
}
