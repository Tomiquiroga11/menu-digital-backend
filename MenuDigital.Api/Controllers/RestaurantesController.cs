using Microsoft.AspNetCore.Mvc;
using MenuDigital.Api.Models;
using MenuDigital.Api.Services;
using MenuDigital.Api.Services.Interfaces;

namespace MenuDigital.Api.Controllers
{
    [ApiController]
    [Route("api/restaurantes")]
    public class RestaurantesController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;
        private readonly IMenuService _menuService; // <--- AGREGADO

        // Inyectamos ambos servicios en el constructor
        public RestaurantesController(IRestauranteService restauranteService, IMenuService menuService)
        {
            _restauranteService = restauranteService;
            _menuService = menuService;
        }

        // 1. GET: Listar todos
        [HttpGet]
        public async Task<IActionResult> GetRestaurantes()
        {
            var restaurantes = await _restauranteService.GetRestaurantesAsync();
            return Ok(restaurantes);
        }

        // 2. PUT: Actualizar mis datos
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurante(int id, [FromBody] RegistroRestauranteDto dto)
        {
            // Quitamos la validación estricta de ID para evitar errores
            var result = await _restauranteService.UpdateRestauranteAsync(id, dto);
            
            if (!result) return BadRequest("No se pudo actualizar o el usuario no existe.");
            
            return Ok(new { message = "Datos actualizados correctamente" });
        }

        // 3. DELETE: Eliminar cuenta
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurante(int id)
        {
            var result = await _restauranteService.DeleteRestauranteAsync(id);
            if (!result) return BadRequest("Error al eliminar.");
            
            return Ok(new { message = "Cuenta eliminada" });
        }

        // --- ¡ESTE ES EL QUE FALTABA PARA EL MENÚ PÚBLICO! ---
        // Ruta: api/restaurantes/{id}/menu
        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetMenuPublico(int id)
        {
            // Usamos el MenuService para traer el menú completo
            var menu = await _menuService.GetMenuCompletoAsync(id);
            
            if (menu == null) return NotFound("Restaurante no encontrado o sin menú.");
            
            return Ok(menu);
        }
    }
}