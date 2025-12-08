using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Api.Entities;

public class Categoria
{
    [Key]
    public int Id { get; set; }
    [Required] 
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;
    
    [ForeignKey("RestauranteId")]
    public int RestauranteId { get; set; }

    public Restaurante Restaurante { get; set; } = null!;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}