using MenuDigital.Api.Models;
using MenuDigital.Api.Services.Interfaces; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/categorias")]
[Authorize]
public class CategoriasController : ControllerBase
{
    private readonly IMenuService _menuService;

    public CategoriasController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    private int GetRestauranteIdFromToken()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        
        return int.Parse(idClaim.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoria([FromBody] CategoriaCreateDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var nuevaCategoria = await _menuService.CreateCategoriaAsync(dto, restauranteId);

        if (nuevaCategoria == null)
        {
            return BadRequest("No se pudo crear la categoria");
        }
        return CreatedAtAction(nameof(GetCategoria), new {id = nuevaCategoria.Id}, nuevaCategoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaCreateDto dto)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.UpdateCategoriaAsync(id, dto, restauranteId);

        if (!exito)
        {
            return NotFound("La categoria no existe o no pertenece a este restaurante.");
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var exito = await _menuService.DeleteCategoriaAsync(id, restauranteId);

        if (!exito)
        {
            return NotFound("La categoría no existe o no pertenece a este restaurante.");
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoria(int id)
    {
        var restauranteId = GetRestauranteIdFromToken();
        var categoria = await _menuService.GetCategoriaSimpleAsync(id, restauranteId);
        if (categoria == null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }

    [HttpGet]
    public async Task<IActionResult> GetMisCategorias()
    {
        var restauranteId = GetRestauranteIdFromToken();
        var categoriasDtos = await _menuService.GetMisCategoriasAsync(restauranteId);
        
        return Ok(categoriasDtos);
    }
}