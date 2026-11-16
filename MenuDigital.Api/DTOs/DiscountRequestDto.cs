using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.DTOs;

public class DiscountRequestDto
{
    [Range(0, 100)]
    public int DiscountPercentage { get; set; }
}