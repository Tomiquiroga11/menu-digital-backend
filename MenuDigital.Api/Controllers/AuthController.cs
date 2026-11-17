using MenuDigital.Api.DTOs;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AuthController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto dto)
    {
        var response = await _accountService.RegisterAsync(dto);
        if (response == null)
        {
            return Conflict(new { message = "Ya existe una cuenta con ese email."});
        }

        return Created("/api/account", response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto dto)
    {
        var response = await _accountService.LoginAsync(dto);
        if (response == null)
        {
            return Unauthorized(new { message = "Email o contraseña incorrectos."});
        }

        return Ok(response);
    }
 
}