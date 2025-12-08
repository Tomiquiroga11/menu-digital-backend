using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models;

public class ProductoCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    [Required]
    [Range(0.01, 1000000)]
    public decimal Precio { get; set; }
    public bool EstaDestacado { get; set; } =  false;
    
    public string? ImagenUrl { get; set; }
    
    public bool TieneDescuento { get; set; }
    
    public int PorcentajeDescuento { get; set; }
    
    public bool TieneHappyHour { get; set; }
    
    [Required]
    public int CategoriaId { get; set; }
}