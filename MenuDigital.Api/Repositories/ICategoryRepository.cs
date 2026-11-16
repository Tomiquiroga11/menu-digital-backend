using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetByRestaurantIdAsync(int restaurantId);
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}