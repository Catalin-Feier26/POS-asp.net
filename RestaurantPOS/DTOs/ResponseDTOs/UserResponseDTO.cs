using RestaurantPOS.Models;

namespace RestaurantPOS.DTOs.ResponseDTOs;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public UserRoles Role { get; set; }
    
}