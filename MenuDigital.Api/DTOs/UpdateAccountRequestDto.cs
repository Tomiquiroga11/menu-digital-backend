using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.DTOs;

public class UpdateAccountRequestDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string RestaurantName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{10,15}$",
        ErrorMessage = "El teléfono debe tener entre 10 y 15 dígitos, sin espacios ni símbolos (ej: 5493511234567).")]
    public string Phone { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}