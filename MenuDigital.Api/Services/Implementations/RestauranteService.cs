using MenuDigital.Api.Entities;
using MenuDigital.Api.Models;
using MenuDigital.Api.Repositories.Interfaces;
using MenuDigital.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MenuDigital.Api.Services.Implementations;

public class RestauranteService : IRestauranteService
{
    private readonly IRestauranteRepository _restauranteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Restaurante> _passwordHasher;

    public RestauranteService(IRestauranteRepository restauranteRepository,
    IUnitOfWork unitOfWork, 
    IPasswordHasher<Restaurante> passwordHasher)
    {
        _restauranteRepository = restauranteRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ReporteVisitasDto?> GetReporteVisitasAsync(int restauranteId)
    {
        var restaurante = await _restauranteRepository.GetRestauranteByIdAsync(restauranteId);

        if (restaurante == null) return null;

        return new ReporteVisitasDto
        {
            RestauranteId = restaurante.Id,
            NombreRestaurante = restaurante.Nombre,
            TotalVisitas = restaurante.Visitas,
            ImagenUrl = restaurante.ImagenUrl,
            HoraApertura = restaurante.HoraApertura,
            HoraCierre = restaurante.HoraCierre,
            Telefono = restaurante.Telefono,
            HappyHourInicio = restaurante.HappyHourInicio, 
            HappyHourFin = restaurante.HappyHourFin
        };
    }


    public async Task<IEnumerable<RestauranteSimpleDto>> GetRestaurantesAsync()
    {
        var restaurantes = await _restauranteRepository.GetRestaurantes();
        return restaurantes.Select(r => new RestauranteSimpleDto
        {
            Id = r.Id,
            Nombre = r.Nombre,
            ImagenUrl = r.ImagenUrl
        });
    }
    
    public async Task<bool> UpdateRestauranteAsync(int id, RegistroRestauranteDto dto)
    {
        var restaurante = await _restauranteRepository.GetRestauranteByIdAsync(id);
        if (restaurante == null) return false;

        restaurante.Nombre = dto.Nombre;
        restaurante.Email = dto.Email;
        restaurante.ImagenUrl = dto.ImagenUrl;
        restaurante.HoraApertura = dto.HoraApertura;
        restaurante.HoraCierre = dto.HoraCierre;
        restaurante.Telefono = dto.Telefono;
        
        restaurante.HappyHourInicio = dto.HappyHourInicio;
        restaurante.HappyHourFin = dto.HappyHourFin;

        if (!string.IsNullOrEmpty(dto.Password))
        {
             restaurante.PasswordHash = _passwordHasher.HashPassword(restaurante, dto.Password);
        }

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateHappyHourHorarioAsync(int restauranteId, int inicio, int fin)
    {
        var restaurante = await _restauranteRepository.GetRestauranteByIdAsync(restauranteId);
        if (restaurante == null) return false;

        restaurante.HappyHourInicio = inicio;
        restaurante.HappyHourFin = fin;

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteRestauranteAsync(int id)
    {
        var restaurante = await _restauranteRepository.GetRestauranteByIdAsync(id);
        if (restaurante == null) return false;
        _restauranteRepository.DeleteRestaurante(restaurante);
        return await _unitOfWork.SaveChangesAsync();
    }
}