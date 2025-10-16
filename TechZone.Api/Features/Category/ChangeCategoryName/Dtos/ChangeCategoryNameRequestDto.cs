namespace TechZoneV1.Features.Category.ChangeCategoryName.Dtos
{
    public class ChangeCategoryNameRequestDto
    {
        public int Id { get; set; }
        public string NewName { get; set; } = string.Empty;
    }
}
