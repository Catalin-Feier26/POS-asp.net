using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Services.Implementations;

public class TableService : ITableService
{
    private readonly ApplicationDbContext _dbContext;

    public TableService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<TableResponseDTO>> GetAllTables()
    {
        //throw new NotImplementedException();
        return await _dbContext.Tables
            .Select(t => new TableResponseDTO
        {
            Id = t.Id,
            Number = t.TableNumber,
            Status = t.Status,
        }).ToListAsync();
    }

    public async Task<bool> CreateTableAsync(TableRequestDTO dto)
    {
        //throw new NotImplementedException();
        if (dto.Number <= 0 || string.IsNullOrWhiteSpace(dto.Status))
            return false;
        var table = new Table
        {
            Status = dto.Status,
            TableNumber = dto.Number
        };
        _dbContext.Tables.Add(table);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTableAsync(int id, TableRequestDTO dto)
    {
       // throw new NotImplementedException();
        if (dto.Number <= 0 || string.IsNullOrWhiteSpace(dto.Status))
            return false;
        var table = await _dbContext.Tables.FindAsync(id);
        if (table == null)
            return false;
        table.Status = dto.Status;
        table.TableNumber = dto.Number;
        _dbContext.Tables.Update(table);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTableAsync(int id)
    {
        //throw new NotImplementedException();
        var table = await _dbContext.Tables.FindAsync(id);
        if (table == null)
            return false;
        _dbContext.Tables.Remove(table);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}