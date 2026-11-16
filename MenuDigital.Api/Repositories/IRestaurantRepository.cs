using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<Restaurant?> GetWithMenuAsync(int id);
    Task<Restaurant?> GetByUserIdAsync(int userId);
    Task UpdateAsync(Restaurant restaurant);
}