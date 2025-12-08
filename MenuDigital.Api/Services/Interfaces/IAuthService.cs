using MenuDigital.Api.Models;

namespace MenuDigital.Api.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDto loginDto);
    
    Task<bool> RegisterAsync(RegistroRestauranteDto registroRestauranteDto);
}