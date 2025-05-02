using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs.RequestDTOs;
using RestaurantPOS.Services.Interfaces;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthService _authService;
    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response == null)
            return Unauthorized(new { message = "Invalid username or password" });
        
        return Ok(response);
    }
}