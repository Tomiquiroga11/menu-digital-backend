using MenuDigital.Api.DTOs;

namespace MenuDigital.Api.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>?> GetByRestaurantAsync(int restaurantId);
    Task<CategoryDto?> CreateAsync(int userId, CategoryRequestDto dto);
    Task<CategoryDto?> UpdateAsync(int userId, int categoryId, CategoryRequestDto dto);
    Task<bool> DeleteAsync(int userId, int categoryId);
}