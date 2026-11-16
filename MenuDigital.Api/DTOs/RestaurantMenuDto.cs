namespace MenuDigital.Api.DTOs;

public class RestaurantMenuDto : RestaurantDto
{
    public List<CategoryMenuDto> Categories { get; set; } = new();
}