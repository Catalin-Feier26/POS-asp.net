using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPOS.Models;

public class OrderItem
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }

    [ForeignKey("OrderId")] public Order Order { get; set; } = null!;
    
    [Required]
    public int MenuItemId { get; set; }
    [ForeignKey("MenuItemId")]
    public MenuItem MenuItem { get; set; } = null!;
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal UnitPrice { get; set; }
}