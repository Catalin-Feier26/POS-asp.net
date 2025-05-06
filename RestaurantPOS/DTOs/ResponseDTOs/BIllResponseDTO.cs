namespace RestaurantPOS.DTOs.ResponseDTOs;

public class BillResponseDTO
{
    public int OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponseDTO> Items { get; set; } = new();
}
