using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Mappings;
using MenuDigital.Api.Repositories;

namespace MenuDigital.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IRestaurantRepository restaurantRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<List<ProductDto>?> GetByCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
        {
            return null;
        }

        var products = await _productRepository.GetByCategoryIdAsync(categoryId);
        return products.Select(p => p.ToDto()).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return product?.ToDto();
    }

    public async Task<ProductDto?> CreateAsync(int userId, ProductRequestDto dto)
    {
        if (!await OwnsCategoryAsync(userId, dto.CategoryId))
        {
            return null;
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            IsFeatured = dto.IsFeatured,
            CategoryId = dto.CategoryId,
        };

        await _productRepository.AddAsync(product);
        return product.ToDto();
    }

    public async Task<ProductDto?> UpdateAsync(int userId, int productId, ProductRequestDto dto)
{
    var product = await GetOwnedProductAsync(userId, productId);
    if (product == null || !await OwnsCategoryAsync(userId, dto.CategoryId))
    {
        return null;
    }

    product.Name = dto.Name;
    product.Description = dto.Description;
    product.Price = dto.Price;
    product.ImageUrl = dto.ImageUrl;
    product.IsFeatured = dto.IsFeatured;
    product.CategoryId = dto.CategoryId;

    await _productRepository.UpdateAsync(product);
    return product.ToDto();
}

    public async Task<ProductDto?> UpdateDiscountAsync(int userId, int productId, DiscountRequestDto dto)
    {
        var product = await GetOwnedProductAsync(userId, productId);
        if (product == null)
        {
            return null;
        }

        product.DiscountPercentage = dto.DiscountPercentage;
        await _productRepository.UpdateAsync(product);
        return product.ToDto();
    }

    public async Task<ProductDto?> UpdateHappyHourAsync(int userId, int productId, HappyHourRequestDto dto)
    {
        var product = await GetOwnedProductAsync(userId, productId);
        if (product == null)
        {
            return null;
        }

        product.IsHappyHour = dto.IsHappyHour;
        await _productRepository.UpdateAsync(product);
        return product.ToDto();
    }

    public async Task<bool> DeleteAsync(int userId, int productId)
    {
        var product = await GetOwnedProductAsync(userId, productId);
        if (product == null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);
        return true;
    }

    private async Task<int?> GetOwnerRestaurantIdAsync(int userId)
    {
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        return restaurant?.Id;
    }

    private async Task<bool> OwnsCategoryAsync(int userId, int categoryId)
    {
        var restaurantId = await GetOwnerRestaurantIdAsync(userId);
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category != null && category.RestaurantId == restaurantId;
    }

    private async Task<Product?> GetOwnedProductAsync(int userId, int productId)
    {
        var restaurantId = await GetOwnerRestaurantIdAsync(userId);
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null || product.Category.RestaurantId != restaurantId)
        {
            return null;
        }

        return product;
    }
}