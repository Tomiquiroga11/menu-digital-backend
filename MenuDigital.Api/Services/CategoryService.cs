using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Mappings;
using MenuDigital.Api.Repositories;

namespace MenuDigital.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public CategoryService(ICategoryRepository categoryRepository, IRestaurantRepository restaurantRepository)
    {
        _categoryRepository = categoryRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<List<CategoryDto>?> GetByRestaurantAsync(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
        {
            return null;
        }

        var categories = await _categoryRepository.GetByRestaurantIdAsync(restaurantId);
        return categories.Select(c => c.ToDto()).ToList();
    }

    public async Task<CategoryDto?> CreateAsync(int userId, CategoryRequestDto dto)
    {
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        if (restaurant == null)
        {
            return null;
        }

        var category = new Category
        {
            Name = dto.Name,
            RestaurantId = restaurant.Id
        };

        await _categoryRepository.AddAsync(category);
        return category.ToDto();
    }

    public async Task<CategoryDto?> UpdateAsync(int userId, int categoryId, CategoryRequestDto dto)
    {
        var category = await GetOwnedCategoryAsync(userId, categoryId);
        if (category == null)
        {
            return null;
        }

        category.Name = dto.Name;
        await _categoryRepository.UpdateAsync(category);
        return category.ToDto();
    }

    public async Task<bool> DeleteAsync(int userId, int categoryId)
    {
        var category = await GetOwnedCategoryAsync(userId, categoryId);
        if (category == null)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(category);
        return true;
    }

    private async Task<Category?> GetOwnedCategoryAsync(int userId, int categoryId)
    {
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        var category = await _categoryRepository.GetByIdAsync(categoryId);   // ← esta línea

        if (restaurant == null || category == null || category.RestaurantId != restaurant.Id)
        {
            return null;
        }

        return category;
    }
}