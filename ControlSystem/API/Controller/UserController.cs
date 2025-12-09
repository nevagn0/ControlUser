using ControkSystem.Application.Services;
using ControlSystem.Application.Services;
using ControlSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
namespace ControlSystem.API.Controller;
using ControlSystem.Application.Dto;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserServices _services;
    private readonly IAuthService _authService;

    public UserController(UserServices services,  IAuthService authService)
    {
        _services = services;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequestDto registerRequestDto)
    {
        try
        {
            await _authService.RegisterAsync(registerRequestDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
           
            HttpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions
            {
                Expires = result.Expires,
                HttpOnly = false,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
           
            return Ok(new
            {
                message = "Успешный вход",
                token = result.Token,
                user = result.User
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}