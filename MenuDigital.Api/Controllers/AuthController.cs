using MenuDigital.Api.Models;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
 private readonly IAuthService _authService;

 public AuthController(IAuthService authService)
 {
  _authService = authService;
 }

 [HttpPost("register")]
 public async Task<IActionResult> Register([FromBody] RegistroRestauranteDto registroRestauranteDto)
 {
  var exito = await _authService.RegisterAsync(registroRestauranteDto);

  if (!exito)
  {
   return BadRequest(new { message = "El email ya se encuentra en uso"});
  }

  return StatusCode(201);
 }

 [HttpPost("login")]
 public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
 {
  var token = await _authService.LoginAsync(loginDto);
  if (token == null)
  {
   return Unauthorized(new { message = "Credenciales inválidas" });
  }
  return Ok(new { Token = token });
 }
}
