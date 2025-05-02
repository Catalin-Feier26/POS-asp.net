using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        var items = await _menuService.GetAllMenuItems();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemRequestDTO dto)
    {
        var success = await _menuService.CreateMenuItem(dto);
        if (!success)
            return BadRequest(new { message = "Invalid input. Please check name, category, and price." });

        return Ok(new { message = "Menu item created successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemRequestDTO dto)
    {
        var success = await _menuService.UpdateMenuItem(id, dto);
        if (!success)
            return BadRequest(new { message = "Menu item not found or invalid input." });

        return Ok(new { message = "Menu item updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        var success = await _menuService.DeleteMenuItem(id);
        if (!success)
            return NotFound(new { message = "Menu item not found." });

        return Ok(new { message = "Menu item deleted successfully." });
    }
}