using MenuDigital.Api.Services;
using MenuDigital.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigital.Api.Controllers
{
    [ApiController]
    [Route("api/cuenta")]
    [Authorize] 
    public class CuentaController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;
        
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
            var exito = await _restauranteService.UpdateRestauranteAsync(id, dto);
            if (!exito) return BadRequest("No se pudo actualizar.");
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCuenta()
        {
            var id = GetRestauranteId();
            var exito = await _restauranteService.DeleteRestauranteAsync(id);
            if (!exito) return BadRequest("No se pudo eliminar.");
            return NoContent();
        }
    }
}