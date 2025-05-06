using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;

    public OrderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<OrderResponseDTO>> GetOrdersByTableAsync(int tableId)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.TableId == tableId)
            .OrderByDescending(o => o.CreatedAt)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .ToListAsync();

        return orders.Select(o => new OrderResponseDTO
        {
            Id = o.Id,
            TableId = o.TableId,
            CreatedAt = o.CreatedAt,
            IsCompleted = o.IsCompleted,
            TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice),
            Items = o.OrderItems.Select(oi => new OrderItemResponseDTO
            {
                Id = oi.Id,
                MenuItemName = oi.MenuItem.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        }).ToList();
    }

    public async Task<bool> CreateOrderAsync(OrderRequestDTO dto)
    {
        var table = await _dbContext.Tables.FindAsync(dto.TableId);
        if (table == null) return false;

        var order = new Order
        {
            TableId = dto.TableId,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddItemToOrderAsync(int orderId, OrderItemRequestDTO dto)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsCompleted);
        var menuItem = await _dbContext.MenuItems.FindAsync(dto.MenuItemId);
        if (order == null || menuItem == null) return false;

        var existingItem = order.OrderItems.FirstOrDefault(oi => oi.MenuItemId == dto.MenuItemId);
        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
        }
        else
        {
            order.OrderItems.Add(new OrderItem
            {
                MenuItemId = dto.MenuItemId,
                Quantity = dto.Quantity,
                UnitPrice = menuItem.Price
            });
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveItemFromOrderAsync(int orderItemId)
    {
        var item = await _dbContext.OrderItems.FindAsync(orderItemId);
        if (item == null) return false;

        _dbContext.OrderItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order == null) return false;

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<BillResponseDTO?> CalculateBillAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null || order.IsCompleted) return null;

        var bill = new BillResponseDTO
        {
            OrderId = order.Id,
            TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice),
            Items = order.OrderItems.Select(oi => new OrderItemResponseDTO
            {
                Id = oi.Id,
                MenuItemName = oi.MenuItem.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };

        order.IsCompleted = true;
        await _dbContext.SaveChangesAsync();
        return bill;
    }
}