using MenuDigital.Api.Models;
using MenuDigital.Api.Services.Interfaces; // Asegúrate que apunte a tu interfaz de Servicio
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/productos")]
[Authorize]

public class ProductosController : ControllerBase
{
    private readonly IMenuService _menuService;

    public ProductosController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    private int GetRestauranteIdFromToken()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(idClaim.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProducto([FromBody] ProductoCreateDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var nuevoProducto = await _menuService.CreateProductoAsync(dto, restauranteId);

        if (nuevoProducto == null)
        {
            return BadRequest("La categoría especificada no es válida o no pertenece a este restaurante.");
        }
        return Created(string.Empty, nuevoProducto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoUpdateDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.UpdateProductoAsync(id, dto, restauranteId);

        if (!exito)
        {
            return NotFound("El producto no existe o no pertenece a este restaurante.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.DeleteProductoAsync(id, restauranteId);

        if (!exito)
        {
            return NotFound("El producto no existe o no pertenece a este restaurante.");
        }

        return NoContent();
    }

    [HttpPatch("{id}/descuento")]
    public async Task<IActionResult> SetDescuento(int id, [FromBody] ProductoDescuentoDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.SetDescuentoAsync(id, dto, restauranteId);

        if (!exito)
        {
            return NotFound("El producto no existe o no pertenece a este restaurante.");
        }

        return NoContent();
    }

    [HttpPatch("{id}/happyhour")]
    public async Task<IActionResult> SetHappyHour(int id, [FromBody] ProductoHappyHourDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.SetHappyHourAsync(id, dto, restauranteId);

        if (!exito)
        {
            return NotFound("El producto no existe o no pertenece a este restaurante.");
        }

        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMisProductos()
    {
        var restauranteId = GetRestauranteIdFromToken();
        var productos = await _menuService.GetMisProductosAsync(restauranteId);
        return Ok(productos);
    }
}