using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models;

public class ProductoHappyHourDto
{
    [Required]
    public bool TieneHappyHour {get; set; }
}