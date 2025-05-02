using System.ComponentModel.DataAnnotations;

namespace RestaurantPOS.Models;

public class MenuItem
{
    [Required]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = "";
    [Required] public string Category { get; set; } = "";
    [Required] public decimal Price { get; set; } = 0.0m;

}