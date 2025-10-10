namespace TechZone.Api.DTOs.Laptop
{
    public class UpdateLaptopDto : CreateLaptopDto
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
    }
}