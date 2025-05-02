using System.ComponentModel.DataAnnotations;
namespace RestaurantPOS.Models;

public class User
{
    public int Id { get; set; }
    [Required] 
    public string Username { get; set; } = "";
    [Required] 
    public string PasswordHash { get; set; } = "";
    [Required] 
    public string Email { get; set; } = "";
    [Required]
    public UserRoles Role { get; set; }
}