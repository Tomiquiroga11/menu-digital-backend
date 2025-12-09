namespace MenuDigital.Api.Models;

public class ReporteVisitasDto
{
    public int RestauranteId { get; set; }
    public string NombreRestaurante { get; set; } = string.Empty;
    public int TotalVisitas { get; set; }
    
    public string? ImagenUrl { get; set; }
    
    public int HoraApertura { get; set; }
    
    public int HoraCierre { get; set; }
    
    public string Telefono { get; set; } = string.Empty;
    
    public int HappyHourInicio { get; set; }
    
    public int HappyHourFin { get; set; }
}