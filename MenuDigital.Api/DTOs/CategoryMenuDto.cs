namespace MenuDigital.Api.DTOs;

public class CategoryMenuDto : CategoryDto
{
    public List<ProductDto> Products { get; set; } = new();
}