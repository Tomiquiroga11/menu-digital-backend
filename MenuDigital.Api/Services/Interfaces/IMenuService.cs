using MenuDigital.Api.Entities;
using MenuDigital.Api.Models;

namespace MenuDigital.Api.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuCompletoDto?> GetMenuCompletoAsync(int restauranteId);

        Task<CategoriaDto?> CreateCategoriaAsync(CategoriaCreateDto dto, int restauranteId);
        
        Task<bool> UpdateCategoriaAsync(int categoriaId, CategoriaCreateDto dto, int restauranteId);
        Task<bool> DeleteCategoriaAsync(int categoriaId, int restauranteId);

        Task<CategoriaDto?> GetCategoriaSimpleAsync(int categoriaId, int restauranteId);

        Task<ProductoDto?> CreateProductoAsync(ProductoCreateDto dto, int restauranteId);
        
        Task<bool> UpdateProductoAsync(int productoId, ProductoUpdateDto dto, int restauranteId);
        Task<bool> DeleteProductoAsync(int productoId, int restauranteId);
        Task<bool> SetDescuentoAsync(int productoId, ProductoDescuentoDto dto, int restauranteId);
        Task<bool> SetHappyHourAsync(int productoId, ProductoHappyHourDto dto, int restauranteId);
        
        Task<IEnumerable<CategoriaDto>> GetMisCategoriasAsync(int restauranteId);
        
        Task<IEnumerable<ProductoDto>> GetMisProductosAsync(int restauranteId);
        
        Task<ProductoDto?> GetProductoPorIdAsync(int productoId, int restauranteId);
        
        Task<bool> UpdateHappyHourHorarioAsync(int restauranteId, int inicio, int fin);
    }
}