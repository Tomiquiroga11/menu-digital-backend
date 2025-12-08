using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Api.Entities;

public class Producto
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    [Required]
    public decimal Precio { get; set; }
    public bool EstaDestacado { get; set; } = false;
    public bool TieneDescuento { get; set; } = false;
    public int PorcentajeDescuento { get; set; } = 0;
    public bool TieneHappyHour {get; set; } = false;
    
    public string? ImagenUrl { get; set; }
    
    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}