using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response == null)
            return Unauthorized(new { message = "Invalid username or password" });
        
        HttpContext.Session.SetString("username", response.Username);
        HttpContext.Session.SetString("role", response.Role);
        
        return Ok(response);
    }
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
    {
        var result = await _authService.RegisterAsync(registerRequest);
        if(!result)
            return Unauthorized(new { message = "Invalid username or password" });
        return Ok(new { message = "User registered successfully" });
    }
}