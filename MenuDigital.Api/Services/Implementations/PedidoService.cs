using System.Text;
using System.Web; // Necesario para HttpUtility.UrlEncode
using MenuDigital.Api.Models;
using MenuDigital.Api.Repositories.Interfaces;
using MenuDigital.Api.Services.Interfaces;

namespace MenuDigital.Api.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        private readonly IRestauranteRepository _restauranteRepo;

        public PedidoService(IRestauranteRepository restauranteRepo)
        {
            _restauranteRepo = restauranteRepo;
        }

        public async Task<RespuestaPedidoDto> GenerarLinkPedidoAsync(SolicitudPedidoDto pedido)
        {
            // 1. Obtener datos del restaurante
            var restaurante = await _restauranteRepo.GetRestauranteByIdAsync(pedido.RestauranteId);
            if (restaurante == null) 
                return new RespuestaPedidoDto { LocalAbierto = false, Mensaje = "Restaurante no encontrado." };

            // 2. Verificar Horario (Hora del Servidor)
            var horaActual = DateTime.Now.Hour;
            bool estaAbierto = horaActual >= restaurante.HoraApertura && horaActual < restaurante.HoraCierre;

            if (!estaAbierto)
            {
                return new RespuestaPedidoDto 
                { 
                    LocalAbierto = false, 
                    Mensaje = $"El local está cerrado. Horario de atención: {restaurante.HoraApertura}:00 a {restaurante.HoraCierre}:00." 
                };
            }

            // 3. Armar el mensaje de WhatsApp
            var sb = new StringBuilder();
            sb.AppendLine($"Hola *{restaurante.Nombre}*, quisiera realizar el siguiente pedido:");
            sb.AppendLine("");
            
            foreach (var item in pedido.Items)
            {
                sb.AppendLine($"- {item.Cantidad}x {item.NombreProducto} (${item.Subtotal})");
            }
            
            sb.AppendLine("");
            sb.AppendLine($"*Total: ${pedido.Total}*");
            sb.AppendLine("Espero su confirmación. Gracias.");

            // 4. Generar URL
            // HttpUtility requiere System.Web (o System.Net.WebUtility en .NET Core)
            string mensajeCodificado = System.Net.WebUtility.UrlEncode(sb.ToString());
            string url = $"https://wa.me/{restaurante.Telefono}?text={mensajeCodificado}";

            return new RespuestaPedidoDto
            {
                LocalAbierto = true,
                LinkWhatsApp = url,
                Mensaje = "Pedido generado exitosamente."
            };
        }
    }
}