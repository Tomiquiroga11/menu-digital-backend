using MenuDigital.Api.Models;

namespace MenuDigital.Api.Services;

public interface IRestauranteService
{
    Task<IEnumerable<RestauranteSimpleDto>> GetRestaurantesAsync();
    
    Task<ReporteVisitasDto?> GetReporteVisitasAsync(int restauranteId);
    
    Task<bool> UpdateRestauranteAsync(int id, RegistroRestauranteDto dto);
    Task<bool> DeleteRestauranteAsync(int id);
}