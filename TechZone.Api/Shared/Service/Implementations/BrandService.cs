using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechZone.Domain.DTOs.Brand;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZone.Domain.Service.Interfaces;
using TechZone.Domain.ServiceResponse;
using TechZone.Infrastructure.Application;

namespace TechZone.Shared.Service.Implementations
{
    public class BrandService : IBrandService
    {
        //private readonly ApplicationDbContext _db;
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly ILogger<BrandService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Cloudinary? _cloudinary;

        public BrandService(ApplicationDbContext db, ILogger<BrandService> logger,
            IBaseRepository<Brand> brandRepository,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;

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
                var query = _brandRepository.GetAll();

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

                var items =  query.Skip(skip).Take(request.PageSize)
                    .Select(b => new BrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Country = b.Country ?? string.Empty,
                        LogoUrl = b.LogoUrl ?? string.Empty,
                        Description = b.Description ?? string.Empty
                    });

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
                var dto = await
                _brandRepository.GetAll()
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

                bool exists = await _brandRepository.GetAll().AnyAsync(b => b.Name.ToLower() == name.ToLower(), ct);
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

                await _brandRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

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
                var entity = await _brandRepository.FirstOrDefaultAsync(b => b.Id == id);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Brand not found", "الماركة غير موجودة");

                var newName = (dto.Name ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(newName))
                    return ServiceResponse<object>.ErrorResponse("Name is required", "الاسم مطلوب", 400);

                var nameTaken = _brandRepository.GetAll().Where(b => b.Id != id && b.Name.ToLower() == newName.ToLower());
                if (nameTaken.Any())
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

                await _unitOfWork.SaveChangesAsync();
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
                var entity = await _brandRepository.FirstOrDefaultAsync(b => b.Id == id);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Brand not found", "الماركة غير موجودة");

                bool inUse = _brandRepository.GetAll().Where(l => l.Id == id).Any();
                if (inUse)
                    return ServiceResponse<object>.ConflictResponse("Cannot delete brand because it has related laptops.", "لا يمكن حذف الماركة لوجود لابتوبات مرتبطة بها");

                _brandRepository.Delete(entity);
                await _unitOfWork.SaveChangesAsync();

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
                var data =await _brandRepository.GetAll()
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
