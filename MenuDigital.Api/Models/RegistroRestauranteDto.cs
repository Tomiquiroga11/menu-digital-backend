using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models;

public class RegistroRestauranteDto
{
    [Required] 
    [MaxLength(50)] 
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [Compare("Password",  ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmarPassword { get; set; } = string.Empty;
    
    public string? ImagenUrl { get; set; }
    
    public int HoraApertura { get; set; }
    
    public int HoraCierre { get; set; }
    
    public string Telefono { get; set; } = string.Empty;
}