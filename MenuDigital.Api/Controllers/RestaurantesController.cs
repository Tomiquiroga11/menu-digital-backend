using MenuDigital.Api.Services;
using MenuDigital.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/restaurantes")]
public class RestaurantesController : ControllerBase
{
    private readonly IRestauranteService _restauranteService;
    private readonly IMenuService _menuService;

    public RestaurantesController(IRestauranteService restauranteService, IMenuService menuService)
    {
        _restauranteService = restauranteService;
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurantes()
    {
        var restaurantes = await _restauranteService.GetRestaurantesAsync();
        return Ok(restaurantes);
    }

    [HttpGet("{id}/menu")]
    public async Task<IActionResult> GetMenu(int id)
    {
        var menu = await _menuService.GetMenuCompletoAsync(id);
        if (menu == null)
        {
            return NotFound();
        }
        return Ok(menu);
    }
}