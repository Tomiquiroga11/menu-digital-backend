using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.DTOs;

public class ProductRequestDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 10_000_000)]
    public decimal Price { get; set; }

    [Url]
    public string? ImageUrl { get; set; }

    public bool IsFeatured { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}