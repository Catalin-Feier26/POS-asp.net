using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("table/{tableId}")]
    public async Task<IActionResult> GetOrdersByTable(int tableId)
    {
        var orders = await _orderService.GetOrdersByTableAsync(tableId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDTO dto)
    {
        var success = await _orderService.CreateOrderAsync(dto);
        if (!success) return BadRequest(new { message = "Invalid table ID." });

        return Ok(new { message = "Order created successfully." });
    }

    [HttpPost("{orderId}/add-item")]
    public async Task<IActionResult> AddItemToOrder(int orderId, [FromBody] OrderItemRequestDTO dto)
    {
        var success = await _orderService.AddItemToOrderAsync(orderId, dto);
        if (!success) return BadRequest(new { message = "Invalid order or menu item." });

        return Ok(new { message = "Item added to order." });
    }

    [HttpDelete("remove-item/{orderItemId}")]
    public async Task<IActionResult> RemoveItemFromOrder(int orderItemId)
    {
        var success = await _orderService.RemoveItemFromOrderAsync(orderItemId);
        if (!success) return NotFound(new { message = "Order item not found." });

        return Ok(new { message = "Item removed from order." });
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var success = await _orderService.DeleteOrderAsync(orderId);
        if (!success) return NotFound(new { message = "Order not found." });

        return Ok(new { message = "Order deleted successfully." });
    }

    [HttpGet("{orderId}/calculate-bill")]
    public async Task<IActionResult> CalculateBill(int orderId)
    {
        var bill = await _orderService.CalculateBillAsync(orderId);
        if (bill == null) return BadRequest(new { message = "Invalid or completed order." });

        return Ok(bill);
    }
}
