namespace MenuDigital.Api.Entities
{

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsHappyHour { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
}