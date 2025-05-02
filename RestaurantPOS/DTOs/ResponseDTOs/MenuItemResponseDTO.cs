namespace RestaurantPOS.DTOs.ResponseDTOs;

public class MenuItemResponseDTO
{
    public int Id{get;set;}
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; } = 0.0m;
}