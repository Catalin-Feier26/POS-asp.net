namespace RestaurantPOS.Models;

public class Table
{
    public int Id { get; set; }
    public int TableNumber { get; set; }
    public string Status { get; set; } = "Free";
}