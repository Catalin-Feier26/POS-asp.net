using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;

namespace RestaurantPOS.Services.Interfaces;

public interface IMenuService
{
    Task<List<MenuItemResponseDTO>> GetAllMenuItems();
    Task<bool> CreateMenuItem(MenuItemRequestDTO menuItem);
    Task<bool> UpdateMenuItem(int id,MenuItemRequestDTO menuItem);
    Task<bool> DeleteMenuItem(int id);

}