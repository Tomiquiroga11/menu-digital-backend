using System.ComponentModel.DataAnnotations;
namespace MenuDigital.Api.Entities;

public class Restaurante
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public int Visitas { get; set; } = 0;
    
    public string? ImagenUrl { get; set; }
    
    public string Telefono { get; set; } = "5491100000000"; 
    
    public int HoraApertura { get; set; } = 9; 
    
    public int HoraCierre { get; set; } = 23;
    
    public int HappyHourInicio { get; set; } = 17; 
    
    public int HappyHourFin { get; set; } = 23;
    
    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}