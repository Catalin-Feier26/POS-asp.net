namespace RestaurantPOS.DTOs.ResponseDTOs;

using System;

public class OrderResponseDTO
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemResponseDTO> Items { get; set; } = new();
}