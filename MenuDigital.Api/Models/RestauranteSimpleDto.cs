namespace MenuDigital.Api.Models;

public class RestauranteSimpleDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
    public string? ImagenUrl { get; set; }
}