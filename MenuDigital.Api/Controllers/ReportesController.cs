using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MenuDigital.Api.Services;

namespace MenuDigital.Api.Controllers;

[ApiController]
[Route("api/reportes")]
[Authorize]
public class ReportesController : ControllerBase
{
    private readonly IRestauranteService _restauranteService;

    public ReportesController(IRestauranteService restauranteService)
    {
        _restauranteService = restauranteService;
    }

    private int GetRestauranteIdFromToken()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(idClaim.Value);
    }

    [HttpGet("visitas")]
    public async Task<IActionResult> GetReporteVisitas()
    {
        var restauranteId = GetRestauranteIdFromToken();
        var reporte = await _restauranteService.GetReporteVisitasAsync(restauranteId);

        if (reporte == null)
        {
            return NotFound();
        }
        return Ok(reporte);
    }
}