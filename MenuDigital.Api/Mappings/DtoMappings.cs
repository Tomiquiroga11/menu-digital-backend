using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Mappings;

public static class DtoMappings
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            FinalPrice = product.Price * (100 - product.DiscountPercentage) / 100m,
            ImageUrl = product.ImageUrl,
            IsFeatured = product.IsFeatured,
            DiscountPercentage = product.DiscountPercentage,
            IsHappyHour = product.IsHappyHour,
            CategoryId = product.CategoryId
        };
    }

    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static RestaurantDto ToDto(this Restaurant restaurant, bool isOpen)
    {
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            Phone = restaurant.Phone,
            OpeningTime = restaurant.OpeningTime,
            ClosingTime = restaurant.ClosingTime,
            IsOpen = isOpen
        };
    }

    public static RestaurantMenuDto ToMenuDto(this Restaurant restaurant, bool isOpen)
    {
        return new RestaurantMenuDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            Phone = restaurant.Phone,
            OpeningTime = restaurant.OpeningTime,
            ClosingTime = restaurant.ClosingTime,
            IsOpen = isOpen,
            Categories = restaurant.Categories.Select(c => new CategoryMenuDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => p.ToDto()).ToList()
            }).ToList()
        };
    }
}