using MenuDigital.Api.Services;
using MenuDigital.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigital.Api.Controllers
{
    [ApiController]
    [Route("api/cuenta")]
    [Authorize] // Solo dueños logueados
    public class CuentaController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;
        // Necesitarás agregar métodos de Update y Delete en tu servicio si no existen
        // Por simplicidad del ejemplo, asumo que los agregarás al IRestauranteService

        public CuentaController(IRestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        private int GetRestauranteId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCuenta([FromBody] RegistroRestauranteDto dto)
        {
            var id = GetRestauranteId();
            // Implementa UpdateRestauranteAsync en tu servicio
            var exito = await _restauranteService.UpdateRestauranteAsync(id, dto);
            if (!exito) return BadRequest("No se pudo actualizar.");
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCuenta()
        {
            var id = GetRestauranteId();
            // Implementa DeleteRestauranteAsync en tu servicio
            var exito = await _restauranteService.DeleteRestauranteAsync(id);
            if (!exito) return BadRequest("No se pudo eliminar.");
            return NoContent();
        }
    }
}