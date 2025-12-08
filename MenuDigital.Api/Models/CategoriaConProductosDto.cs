namespace MenuDigital.Api.Models;

public class CategoriaConProductosDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<ProductoDto> Productos { get; set; } = new List<ProductoDto>();
}