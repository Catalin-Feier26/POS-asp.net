using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Services.Implementations;

public class MenuService:IMenuService
{
    private readonly ApplicationDbContext _dbContext;

    public MenuService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<MenuItemResponseDTO>> GetAllMenuItems()
    {
        //throw new NotImplementedException();
        return await _dbContext.MenuItems
            .Select(m => new MenuItemResponseDTO
            {
                Id = m.Id,
                Name = m.Name,
                Category = m.Category,
                Price = decimal.Round(m.Price,2),
            }).ToListAsync();
    }

    public async Task<bool> CreateMenuItem(MenuItemRequestDTO menuItem)
    {
        //throw new NotImplementedException();
        if (string.IsNullOrWhiteSpace(menuItem.Name) || menuItem.Price < 0 || string.IsNullOrWhiteSpace(menuItem.Category))
            return false;

        var menuitem = new MenuItem
        {
            Name = menuItem.Name,
            Category = menuItem.Category,
            Price = menuItem.Price,
        };
        
        _dbContext.MenuItems.Add(menuitem);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateMenuItem(int id, MenuItemRequestDTO menuItem)
    {
        //throw new NotImplementedException();
        var menuitem = await _dbContext.MenuItems.FindAsync(id);
        if (menuitem == null)
            return false;
        if (string.IsNullOrWhiteSpace(menuItem.Name) || menuItem.Price < 0 || string.IsNullOrWhiteSpace(menuItem.Category))
            return false;

        menuitem.Name = menuItem.Name;
        menuitem.Category = menuItem.Category;
        menuitem.Price = menuItem.Price;
        
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMenuItem(int id)
    {
        //throw new NotImplementedException();
        var menuItem = await _dbContext.MenuItems.FindAsync(id);
        if(menuItem==null)
            return false;
        
        _dbContext.MenuItems.Remove(menuItem);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}