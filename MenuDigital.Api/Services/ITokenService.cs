using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Services;

public interface ITokenService
{
    string CreateToken(User user);
}