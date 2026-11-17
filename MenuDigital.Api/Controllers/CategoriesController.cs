using MenuDigital.Api.DTOs;
using MenuDigital.Api.Extensions;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public CategoriesController(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    [HttpGet("{id:int}/products")]
    public async Task<ActionResult<List<ProductDto>>> GetProducts(int id)
    {
        var products = await _productService.GetByCategoryAsync(id);
        if (products == null)
        {
            return NotFound();
        }
        
        return Ok(products);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(CategoryRequestDto dto)
    {
        var category = await _categoryService.CreateAsync(User.GetUserId(), dto);
        if (category == null)
        {
            return NotFound(new { message = "No se encontró tu restaurante." });
        }

        return StatusCode(StatusCodes.Status201Created, category);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Update(int id, CategoryRequestDto dto)
    {
        var category = await _categoryService.UpdateAsync(User.GetUserId(), id, dto);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(User.GetUserId(), id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}