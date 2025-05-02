namespace RestaurantPOS.DTOs.RequestDTOs;

public class MenuItemRequestDTO
{
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; } = 0.0m;
}