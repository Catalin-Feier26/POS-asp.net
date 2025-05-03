using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;

namespace RestaurantPOS.Services.Interfaces;

public interface ITableService
{
    Task<List<TableResponseDTO>> GetAllTables();
    Task<bool> CreateTableAsync(TableRequestDTO dto);
    Task<bool> UpdateTableAsync(int id,TableRequestDTO dto);
    Task<bool> DeleteTableAsync(int id);
}