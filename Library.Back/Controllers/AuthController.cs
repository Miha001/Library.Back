using Library.Repositories.Database.Entities;
using LibraryService.Models;
using LibraryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers;

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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid username or password.");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var passwordsEquals = request.Password == request.ConfirmPassword;
        if (passwordsEquals)
        {
            var success = await _authService.RegisterAsync(request);
            if (success)
            {
                return Ok();
            }
        }
        if (!passwordsEquals)
        {
            return BadRequest("Passwords not equals.");
        }
        return Conflict();
    }
}