using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;


namespace RestaurantPOS.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponseDTO>> GetOrdersByTableAsync(int tableId);
    Task<bool> CreateOrderAsync(OrderRequestDTO dto);
    Task<bool> AddItemToOrderAsync(int orderId, OrderItemRequestDTO dto);
    Task<bool> RemoveItemFromOrderAsync(int orderItemId);
    Task<bool> DeleteOrderAsync(int orderId);
    Task<BillResponseDTO?> CalculateBillAsync(int orderId);
}