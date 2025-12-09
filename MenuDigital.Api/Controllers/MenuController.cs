using Microsoft.AspNetCore.Mvc;
using MenuDigital.Api.Models;
using MenuDigital.Api.Services.Interfaces;

namespace MenuDigital.Api.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // ==========================================
        //               CATEGORÍAS
        // ==========================================

        [HttpGet("{restauranteId}/categorias")]
        public async Task<IActionResult> GetCategorias(int restauranteId)
        {
            var categorias = await _menuService.GetMisCategoriasAsync(restauranteId);
            return Ok(categorias);
        }

        [HttpGet("{restauranteId}/categorias/{id}")]
        public async Task<IActionResult> GetCategoriaById(int restauranteId, int id)
        {
            var cat = await _menuService.GetCategoriaSimpleAsync(id, restauranteId);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpPost("{restauranteId}/categorias")]
        public async Task<IActionResult> CreateCategoria(int restauranteId, [FromBody] CategoriaCreateDto dto)
        {
            var result = await _menuService.CreateCategoriaAsync(dto, restauranteId);
            if (result == null) return BadRequest("No se pudo crear.");
            return Ok(result);
        }

        [HttpPut("{restauranteId}/categorias/{categoriaId}")]
        public async Task<IActionResult> UpdateCategoria(int restauranteId, int categoriaId, [FromBody] CategoriaCreateDto dto)
        {
            var result = await _menuService.UpdateCategoriaAsync(categoriaId, dto, restauranteId);
            if (!result) return BadRequest("Error al actualizar.");
            return Ok(new { message = "Categoría actualizada" });
        }

        [HttpDelete("{restauranteId}/categorias/{categoriaId}")]
        public async Task<IActionResult> DeleteCategoria(int restauranteId, int categoriaId)
        {
            var result = await _menuService.DeleteCategoriaAsync(categoriaId, restauranteId);
            if (!result) return BadRequest("Error al eliminar.");
            return Ok(new { message = "Categoría eliminada" });
        }

        // ==========================================
        //               PRODUCTOS
        // ==========================================

        [HttpGet("{restauranteId}/mis-productos")]
        public async Task<IActionResult> GetMisProductos(int restauranteId)
        {
            var productos = await _menuService.GetMisProductosAsync(restauranteId);
            return Ok(productos);
        }

        [HttpGet("{restauranteId}/productos/{productoId}")]
        public async Task<IActionResult> GetProducto(int restauranteId, int productoId)
        {
            var producto = await _menuService.GetProductoPorIdAsync(productoId, restauranteId);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost("{restauranteId}/productos")]
        public async Task<IActionResult> CreateProducto(int restauranteId, [FromBody] ProductoCreateDto dto)
        {
            var result = await _menuService.CreateProductoAsync(dto, restauranteId);
            if (result == null) return BadRequest("Error al crear producto.");
            return Ok(result);
        }

        [HttpPut("{restauranteId}/productos/{productoId}")]
        public async Task<IActionResult> UpdateProducto(int restauranteId, int productoId, [FromBody] ProductoUpdateDto dto)
        {
            var result = await _menuService.UpdateProductoAsync(productoId, dto, restauranteId);
            if (!result) return BadRequest("Error al actualizar.");
            return Ok(new { message = "Producto actualizado" });
        }

        [HttpDelete("{restauranteId}/productos/{productoId}")]
        public async Task<IActionResult> DeleteProducto(int restauranteId, int productoId)
        {
            var result = await _menuService.DeleteProductoAsync(productoId, restauranteId);
            if (!result) return BadRequest("Error al eliminar.");
            return Ok(new { message = "Producto eliminado" });
        }

        // ==========================================
        //           SWITCHES & CONFIGURACIÓN
        // ==========================================
        
        [HttpPatch("{restauranteId}/productos/{id}/descuento")]
        public async Task<IActionResult> SetDescuento(int restauranteId, int id, [FromBody] ProductoDescuentoDto dto)
        {
             var res = await _menuService.SetDescuentoAsync(id, dto, restauranteId);
             if(!res) return BadRequest();
             return Ok();
        }

        [HttpPatch("{restauranteId}/productos/{id}/happy-hour")]
        public async Task<IActionResult> SetHappyHour(int restauranteId, int id, [FromBody] ProductoHappyHourDto dto)
        {
             var res = await _menuService.SetHappyHourAsync(id, dto, restauranteId);
             if(!res) return BadRequest();
             return Ok();
        }

        // --- ¡ESTE ES EL QUE TE FALTABA PARA SOLUCIONAR EL 404! ---
        [HttpPatch("{restauranteId}/happy-hour-settings")]
        public async Task<IActionResult> UpdateHappyHourSettings(int restauranteId, [FromBody] HappyHourRangoDto dto)
        {
            var result = await _menuService.UpdateHappyHourHorarioAsync(restauranteId, dto.Inicio, dto.Fin);
            if (!result) return BadRequest("No se pudo actualizar el horario.");
            
            return Ok(new { message = "Horario actualizado correctamente" });
        }
    }
}