namespace MenuDigital.Api.Models;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool EstaDestacado { get; set; }
    public bool TieneDescuento { get; set; }
    public int PorcentajeDescuento { get; set; }
    public bool TieneHappyHour { get; set; }
    
    public int CategoriaId { get; set; }
    
    public string? ImagenUrl { get; set; }
    
    public decimal? PrecioOriginal { get; set; } 
    
    public bool EnHappyHourAhora { get; set; }
}