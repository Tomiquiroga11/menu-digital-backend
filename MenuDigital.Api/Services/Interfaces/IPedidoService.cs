using MenuDigital.Api.Models;

namespace MenuDigital.Api.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<RespuestaPedidoDto> GenerarLinkPedidoAsync(SolicitudPedidoDto pedido);
    }
}