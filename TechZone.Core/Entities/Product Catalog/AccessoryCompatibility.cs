namespace TechZone.Core.Entities
{
    public class AccessoryCompatibility : BaseEntity
    {
        public int AccessoryId { get; set; }
        public int LaptopId { get; set; }

        // Navigation
        public Accessory Accessory { get; set; }
        public Laptop Laptop { get; set; }
    }
}