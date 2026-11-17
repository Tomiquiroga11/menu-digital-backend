using MenuDigital.Api.DTOs;
using MenuDigital.Api.Extensions;
using MenuDigital.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create (ProductRequestDto dto)
    {
        var product = await _productService.CreateAsync(User.GetUserId(), dto);
        if (product == null)
        {
            return BadRequest(new { message = "La categoría no existe o no pertenece a tu restaurante."});
        }

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDto>> Update (int id, ProductRequestDto dto)
    {
        var product = await _productService.UpdateAsync(User.GetUserId(), id, dto);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPatch("{id:int}/discount")]
    public async Task<ActionResult<ProductDto>> UpdateDiscount(int id, DiscountRequestDto dto)
    {
        var product = await _productService.UpdateDiscountAsync(User.GetUserId(), id, dto);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPatch("{id:int}/happy-hour")]
    public async Task<ActionResult<ProductDto>> UpdateHappyHour(int id, HappyHourRequestDto dto)
    {
        var product = await _productService.UpdateHappyHourAsync(User.GetUserId(), id, dto);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(User.GetUserId(), id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}