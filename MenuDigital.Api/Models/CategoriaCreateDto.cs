using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.Models;

public class CategoriaCreateDto
{
    [Required] [MaxLength(50)] public string Nombre { get; set; } = string.Empty;
}