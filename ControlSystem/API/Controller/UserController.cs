using ControkSystem.Application.Services;
using ControlSystem.Domain.Repository;
using ControlSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ControlSystem.API.Controller;
using ControlSystem.Application.Dto;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public UserController( IAuthService authService,  IUserRepository userRepository)
    {
        _userRepository =  userRepository;
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
                HttpOnly = true,
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
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTime.UtcNow.AddYears(-1)
            };

            Response.Cookies.Append("access_token", "", cookieOptions);
                
            return Ok(new { 
                message = "Успешный выход"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpGet("GetAllUsers")]
    [Authorize(Policy = "GetAllUsers")]
    public async Task<OkObjectResult> GetALlUsers()
    {
            var user = await _userRepository.GetAllAsync();
            return Ok(user);
    }
}