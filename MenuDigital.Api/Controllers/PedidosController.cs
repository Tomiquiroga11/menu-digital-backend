using MenuDigital.Api.Models;
using MenuDigital.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MenuDigital.Api.Controllers;

    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost("whatsapp")]
        public async Task<IActionResult> GenerarPedido([FromBody] SolicitudPedidoDto pedido)
        {
            var respuesta = await _pedidoService.GenerarLinkPedidoAsync(pedido);
            return Ok(respuesta);
        }
    }
