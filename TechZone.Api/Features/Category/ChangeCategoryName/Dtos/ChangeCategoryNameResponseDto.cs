namespace TechZoneV1.Features.Category.ChangeCategoryName.Dtos
{
    public class ChangeCategoryNameResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? oldname { get; set; }

    }
}
