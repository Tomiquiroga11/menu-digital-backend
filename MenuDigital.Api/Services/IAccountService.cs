using MenuDigital.Api.DTOs;

namespace MenuDigital.Api.Services;

public interface IAccountService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
}