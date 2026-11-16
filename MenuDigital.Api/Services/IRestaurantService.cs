using MenuDigital.Api.DTOs;

namespace MenuDigital.Api.Services;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetAllAsync();
    Task<RestaurantMenuDto?> GetMenuAsync(int restaurantId);
}