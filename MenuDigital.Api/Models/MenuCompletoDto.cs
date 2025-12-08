namespace MenuDigital.Api.Models;

public class MenuCompletoDto
{
    public int IdRestaurante { get; set; }
    public string NombreRestaurante { get; set; }
    public List<CategoriaConProductosDto> Categorias { get; set; } = new List<CategoriaConProductosDto>();
}