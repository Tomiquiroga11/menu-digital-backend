namespace MenuDigital.Api.Models
{
    public class ItemPedidoDto
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class SolicitudPedidoDto
    {
        public int RestauranteId { get; set; }
        public List<ItemPedidoDto> Items { get; set; } = new List<ItemPedidoDto>();
        public decimal Total { get; set; }
    }

    public class RespuestaPedidoDto
    {
        public bool LocalAbierto { get; set; }
        public string? LinkWhatsApp { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}