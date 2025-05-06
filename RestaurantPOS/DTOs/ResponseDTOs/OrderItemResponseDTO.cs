namespace RestaurantPOS.DTOs.ResponseDTOs;

public class OrderItemResponseDTO
{
    public int Id { get; set; }
    public string MenuItemName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}
