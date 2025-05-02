using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.DTOs.ResponseDTOs;
using RestaurantPOS.Models;
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

    public async Task<bool> RegisterAsync(RegisterRequestDTO registerRequest)
    {
        if (await _dbContext.Users.AnyAsync(u =>
                u.Username == registerRequest.Username || u.Email == registerRequest.Email))
            return false;
        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
            Role = UserRoles.Server
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}