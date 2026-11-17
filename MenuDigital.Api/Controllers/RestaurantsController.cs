using MenuDigital.Api.DTOs;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly ICategoryService _categoryService;

    public RestaurantsController(IRestaurantService restaurantService, ICategoryService categoryService)
    {
        _restaurantService = restaurantService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetAll()
    {
        return Ok(await _restaurantService.GetAllAsync());
    }

    [HttpGet("{id:int}/menu")]
    public async Task<ActionResult<RestaurantMenuDto>> GetMenu(int id)
    {
        var menu = await _restaurantService.GetMenuAsync(id);
        if (menu == null)
        {
            return NotFound();
        }

        return Ok(menu);
    }

    [HttpGet("{id:int}/categories")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories(int id)
    {
        var categories = await _categoryService.GetByRestaurantAsync(id);
        if (categories == null)
        {
            return NotFound();
        }

        return Ok(categories);
    }

}