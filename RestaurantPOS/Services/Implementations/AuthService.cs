using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
        if (user == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            return null;
        return new LoginResponseDTO
        {
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}