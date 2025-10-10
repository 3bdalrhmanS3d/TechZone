using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechZone.Core.DTOs;
using TechZone.Core.DTOs.Category;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.PagedResult;
using TechZone.Core.Service.Interfaces;
using TechZone.Core.ServiceResponse;
using TechZone.EF.Application;

namespace TechZone.EF.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ApplicationDbContext db, ILogger<CategoryService> logger)
        {
            _db = db;
            _logger = logger;

            var cloudUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            if (!string.IsNullOrWhiteSpace(cloudUrl))
            {
                _cloudinary = new Cloudinary(cloudUrl) { Api = { Secure = true } };
            }
        }


        public async Task<ServiceResponse<PagedResult<CategoryDto>>> GetAllAsync(
            PaginationParamsDto<CategorySortBy> request, CancellationToken ct = default)
        {
            try
            {
                var query = _db.Categories.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    string term = request.Search.Trim().ToLower();
                    query = query.Where(c =>
                        c.Name.ToLower().Contains(term) ||
                        (c.Description ?? string.Empty).ToLower().Contains(term));
                }

                bool desc = request.SortDirection == SortDirection.Desc;
                var sortBy = request.SortBy;

                query = sortBy switch
                {
                    CategorySortBy.Name => desc ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                    _ => desc ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id)
                };

                int total = await query.CountAsync(ct);
                int skip = (request.Page <= 1 ? 0 : (request.Page - 1) * request.PageSize);

                var items = await query.Skip(skip).Take(request.PageSize)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description ?? string.Empty,
                        CategoryImageUrl = c.CategoryImageURL ?? string.Empty
                    })
                    .ToListAsync(ct);

                var page = new PagedResult<CategoryDto>(items, total, request.Page, request.PageSize);
                return ServiceResponse<PagedResult<CategoryDto>>.SuccessResponse(page, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.GetAllAsync");
                return ServiceResponse<PagedResult<CategoryDto>>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<CategoryDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var dto = await _db.Categories.AsNoTracking()
                    .Where(c => c.Id == id)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description ?? string.Empty,
                        CategoryImageUrl = c.CategoryImageURL ?? string.Empty
                    })
                    .FirstOrDefaultAsync(ct);

                if (dto == null)
                    return ServiceResponse<CategoryDto>.NotFoundResponse("Category not found", "التصنيف غير موجود");

                return ServiceResponse<CategoryDto>.SuccessResponse(dto, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.GetByIdAsync");
                return ServiceResponse<CategoryDto>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<CategoryDto>> CreateAsync(CreateCategoryRequest dto, CancellationToken ct = default)
        {
            try
            {
                var name = (dto.Name ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(name))
                    return ServiceResponse<CategoryDto>.ErrorResponse("Name is required", "الاسم مطلوب", 400);

                if (dto.Image is null || dto.Image.Length == 0)
                    return ServiceResponse<CategoryDto>.ErrorResponse("Image is required", "الصورة مطلوبة", 400);

                if (!IsSupportedImage(dto.Image))
                    return ServiceResponse<CategoryDto>.ErrorResponse("Only image files are allowed", "يُسمح بملفات الصور فقط", 400);

                bool exists = await _db.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower(), ct);
                if (exists)
                    return ServiceResponse<CategoryDto>.ConflictResponse($"Category with name '{name}' already exists.", "اسم التصنيف مستخدم بالفعل");

                string imageUrl = await UploadCategoryImageAsync(dto.Image, ct);
                if (string.IsNullOrWhiteSpace(imageUrl))
                    return ServiceResponse<CategoryDto>.ErrorResponse("Image upload failed", "فشل رفع الصورة", 500);

                var entity = new Category
                {
                    Name = name,
                    Description = dto.Description?.Trim() ?? string.Empty,
                    CategoryImageURL = imageUrl
                };

                await _db.Categories.AddAsync(entity, ct);
                await _db.SaveChangesAsync(ct);

                var result = new CategoryDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description ?? string.Empty,
                    CategoryImageUrl = entity.CategoryImageURL ?? string.Empty
                };

                var resp = ServiceResponse<CategoryDto>.SuccessResponse(result, "Created", "تم الإنشاء");
                resp.StatusCode = 201;
                return resp;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in CategoryService.CreateAsync");
                return ServiceResponse<CategoryDto>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.CreateAsync");
                return ServiceResponse<CategoryDto>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateCategoryRequest dto, CancellationToken ct = default)
        {
            try
            {
                var entity = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Category not found", "التصنيف غير موجود");

                var newName = (dto.Name ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(newName))
                    return ServiceResponse<object>.ErrorResponse("Name is required", "الاسم مطلوب", 400);

                bool nameTaken = await _db.Categories.AnyAsync(c => c.Id != id && c.Name.ToLower() == newName.ToLower(), ct);
                if (nameTaken)
                    return ServiceResponse<object>.ConflictResponse($"Category with name '{newName}' already exists.", "اسم التصنيف مستخدم بالفعل");

                entity.Name = newName;
                entity.Description = dto.Description?.Trim() ?? string.Empty;

                if (dto.ClearImage == true)
                {
                    entity.CategoryImageURL = string.Empty;
                }
                else if (dto.Image is not null && dto.Image.Length > 0)
                {
                    if (!IsSupportedImage(dto.Image))
                        return ServiceResponse<object>.ErrorResponse("Only image files are allowed", "يُسمح بملفات الصور فقط", 400);

                    var newUrl = await UploadCategoryImageAsync(dto.Image, ct);
                    if (string.IsNullOrWhiteSpace(newUrl))
                        return ServiceResponse<object>.ErrorResponse("Image upload failed", "فشل رفع الصورة", 500);

                    // (اختياري) احذف القديمة من Cloudinary لو بتخزن الـ public_id
                    entity.CategoryImageURL = newUrl;
                }

                await _db.SaveChangesAsync(ct);
                return ServiceResponse<object>.SuccessResponse(null, "Updated", "تم التحديث");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in CategoryService.UpdateAsync");
                return ServiceResponse<object>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.UpdateAsync");
                return ServiceResponse<object>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<object>> DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
                if (entity == null)
                    return ServiceResponse<object>.NotFoundResponse("Category not found", "التصنيف غير موجود");

                bool inUse = await _db.Laptops.AnyAsync(l => l.CategoryId == id, ct);
                if (inUse)
                    return ServiceResponse<object>.ConflictResponse("Cannot delete category because it has related laptops.", "لا يمكن حذف التصنيف لوجود لابتوبات مرتبطة به");

                _db.Categories.Remove(entity);
                await _db.SaveChangesAsync(ct);

                return ServiceResponse<object>.SuccessResponse(null, "Deleted", "تم الحذف");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DB update error in CategoryService.DeleteAsync");
                return ServiceResponse<object>.ConflictResponse("DB constraint violation", "تعارض في قيود قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.DeleteAsync");
                return ServiceResponse<object>.InternalServerErrorResponse();
            }
        }

        public async Task<ServiceResponse<List<CategoryWithCountDto>>> GetWithLaptopCountsAsync(CancellationToken ct = default)
        {
            try
            {
                var data = await _db.Categories.AsNoTracking()
                    .OrderBy(c => c.Name)
                    .Select(c => new CategoryWithCountDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        LaptopsCount = c.Laptops.Count(),
                        CategoryImageUrl = c.CategoryImageURL ?? string.Empty
                    })
                    .ToListAsync(ct);

                return ServiceResponse<List<CategoryWithCountDto>>.SuccessResponse(data, "Fetched successfully", "تم الجلب بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryService.GetWithLaptopCountsAsync");
                return ServiceResponse<List<CategoryWithCountDto>>.InternalServerErrorResponse();
            }
        }

        private static bool IsSupportedImage(IFormFile file)
            => file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

        private async Task<string> UploadCategoryImageAsync(IFormFile file, CancellationToken ct)
        {
            if (_cloudinary == null) return string.Empty;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "techzone/categories"
            };
            var result = await _cloudinary.UploadAsync(uploadParams, ct);
            return result?.SecureUrl?.ToString() ?? string.Empty;
        }
    }
}
