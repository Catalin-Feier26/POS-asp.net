namespace RestaurantPOS.Services.Implementations;

using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Services.Interfaces;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Models;
using RestaurantPOS.DTOs.ResponseDTOs;

public class UserService :IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateUserAsync(RegisterRequestDTO dto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email || u.Username == dto.Username))
            return false;
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = UserRoles.Server
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        //throw new NotImplementedException();
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return false;

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<List<UserResponseDTO>> getAllUsersAsync()
    {
        return _dbContext.Users
            .Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserRequestDTO dto)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return false;

        if (await _dbContext.Users.AnyAsync(u => u.Id != id && u.Username == dto.Username))
            return false;
        if(await _dbContext.Users.AnyAsync(u => u.Id != id && u.Email == dto.Email))
            return false;
        user.Username = dto.Username;
        user.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
        if (Enum.TryParse<UserRoles>(dto.Role, out var parsedRole))
        {
            user.Role = parsedRole;
        }
        await _dbContext.SaveChangesAsync();
        return true;
    }
}