using MenuDigital.Api.DTOs;

namespace MenuDigital.Api.Services;

public interface IProductService
{
    Task<List<ProductDto>?> GetByCategoryAsync(int categoryId);
    Task<ProductDto?> GetByIdAsync(int productId);
    Task<ProductDto?> CreateAsync(int userId, ProductRequestDto dto);
    Task<ProductDto?> UpdateAsync(int userId, int productId, ProductRequestDto dto);
    Task<ProductDto?> UpdateDiscountAsync(int userId, int productId, DiscountRequestDto dto);
    Task<ProductDto?> UpdateHappyHourAsync(int userId, int productId, HappyHourRequestDto dto);
    Task<bool> DeleteAsync(int userId, int productId);
}