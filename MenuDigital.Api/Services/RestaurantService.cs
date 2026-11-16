using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Mappings;
using MenuDigital.Api.Repositories;

namespace MenuDigital.Api.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<List<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return restaurants.Select(r => r.ToDto(IsOpenNow(r))).ToList();
    }

    public async Task<RestaurantMenuDto?> GetMenuAsync(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetWithMenuAsync(restaurantId);
        if (restaurant == null)
        {
            return null;
        }

        return restaurant.ToMenuDto(IsOpenNow(restaurant));
    }

    private static bool IsOpenNow(Restaurant restaurant)
    {
        var now = TimeOnly.FromDateTime(DateTime.Now);
        return now.IsBetween(restaurant.OpeningTime, restaurant.ClosingTime);
    }
}