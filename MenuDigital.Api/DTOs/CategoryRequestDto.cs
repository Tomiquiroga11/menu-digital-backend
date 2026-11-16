using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.DTOs;

public class CategoryRequestDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}