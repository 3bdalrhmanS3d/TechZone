namespace TechZoneV1.Features.Cart.ClearCart.Dtos
{
    // Response DTO (what the handler returns)
    public class ClearCartResponseDto
    {
        public int ItemsRemoved { get; set; }
        public DateTime ClearedAt { get; set; }
    }
}