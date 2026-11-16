using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetByRestaurantIdAsync(int restaurantId);
    Task<List<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}