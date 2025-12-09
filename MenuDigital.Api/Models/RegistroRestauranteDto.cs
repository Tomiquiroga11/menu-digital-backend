using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models
{
    public class RegistroRestauranteDto
    {
        public int Id { get; set; } 

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? ImagenUrl { get; set; }
        public int HoraApertura { get; set; }
        public int HoraCierre { get; set; }
        public int HappyHourInicio { get; set; }
        public int HappyHourFin { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}