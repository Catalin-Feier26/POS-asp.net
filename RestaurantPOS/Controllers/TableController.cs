using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var tables = await _tableService.GetAllTables();
        return Ok(tables);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable([FromBody] TableRequestDTO dto)
    {
        var success = await _tableService.CreateTableAsync(dto);
        if (!success)
            return BadRequest(new { message = "Invalid table data." });

        return Ok(new { message = "Table created successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTable(int id, [FromBody] TableRequestDTO dto)
    {
        var success = await _tableService.UpdateTableAsync(id, dto);
        if (!success)
            return BadRequest(new { message = "Table not found or invalid data." });

        return Ok(new { message = "Table updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var success = await _tableService.DeleteTableAsync(id);
        if (!success)
            return NotFound(new { message = "Table not found." });

        return Ok(new { message = "Table deleted successfully." });
    }
}