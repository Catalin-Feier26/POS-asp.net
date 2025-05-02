using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;

namespace RestaurantPOS.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponseDTO>> getAllUsersAsync();
    Task<bool>  CreateUserAsync(RegisterRequestDTO dto);
    Task<bool> UpdateUserAsync(int id, UpdateUserRequestDTO dto);
    Task<bool> DeleteUserAsync(int id);
}