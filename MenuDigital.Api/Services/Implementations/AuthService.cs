using MenuDigital.Api.Entities;
using MenuDigital.Api.Models;
using Microsoft.AspNetCore.Identity; 
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text;
using MenuDigital.Api.Repositories.Interfaces;

namespace MenuDigital.Api.Services.Implementations;

public class AuthService  : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRestauranteRepository  _restauranteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Restaurante> _passwordHasher;
    
    public AuthService(IConfiguration configuration,
        IRestauranteRepository restauranteRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher<Restaurante> passwordHasher)
        {
        _configuration = configuration;
        _restauranteRepository = restauranteRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        }

    public async Task<bool> RegisterAsync(RegistroRestauranteDto registroRestauranteDto)
    {
        var restauranteExistente = await _restauranteRepository.GetRestauranteByEmail(registroRestauranteDto.Email);
        if (restauranteExistente != null)
        {
            return false;
        }

        var restaurante = new Restaurante
        {
            Nombre = registroRestauranteDto.Nombre,
            Email = registroRestauranteDto.Email,
            ImagenUrl = registroRestauranteDto.ImagenUrl
        };
        restaurante.PasswordHash = _passwordHasher.HashPassword(restaurante, registroRestauranteDto.Password);
        await _restauranteRepository.AddRestaurante(restaurante);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        var restaurante = await _restauranteRepository.GetRestauranteByEmail(loginDto.Email);
    
        if (restaurante == null)
        {
            return null; 
        }
    
        var resultado = _passwordHasher.VerifyHashedPassword(restaurante, restaurante.PasswordHash, loginDto.Password);
    
        if (resultado == PasswordVerificationResult.Failed)
        {
            return null;
        }
    
        return GenerateJwtToken(restaurante); 
    }

    private string GenerateJwtToken(Restaurante restaurante)
    {
        var secretKeyString = _configuration["Authentication:SecretForKey"];

        if (string.IsNullOrEmpty(secretKeyString))
        {
            throw new ArgumentNullException(nameof(secretKeyString), "Authentication:SecretForKey no está configurada en appsettings.json");
        }
    
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, restaurante.Id.ToString()), 
            new Claim(ClaimTypes.Email, restaurante.Email),
            new Claim(ClaimTypes.Name, restaurante.Nombre)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow, 
            expires: DateTime.UtcNow.AddHours(8), 
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}