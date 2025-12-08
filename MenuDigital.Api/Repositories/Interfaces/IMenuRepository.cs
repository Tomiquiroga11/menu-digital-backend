using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Repositories.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<Categoria>> GetCategoriasConProductosAsync(int restauranteId);
    
    Task<Categoria?> GetCategoriaByIdAsync(int categoriaId);
    void AddCategoria(Categoria categoria);
    void DeleteCategoria(Categoria categoria);
    
    Task<Producto?> GetProductoByIdAsync(int productoId);
    void AddProducto(Producto producto);
    void DeleteProducto(Producto producto);
    
    Task<IEnumerable<Categoria>> GetCategoriasPorRestauranteAsync(int restauranteId);
    
    Task<IEnumerable<Producto>> GetProductosPorRestauranteAsync(int restauranteId);
}   