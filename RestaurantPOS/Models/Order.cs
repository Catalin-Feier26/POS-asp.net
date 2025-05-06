using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPOS.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public int TableId { get; set; }
    [ForeignKey("TableId")]
    public Table Table { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsCompleted { get; set; } = false;

    public List<OrderItem> OrderItems { get; set; } = new();
}