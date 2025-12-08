using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models;

public class ProductoDescuentoDto
{
    [Required]
    public bool TieneDescuento { get; set; }
    [Range(0, 100)]
    public int PorcentajeDescuento { get; set; }
}