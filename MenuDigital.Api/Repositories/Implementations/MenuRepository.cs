using MenuDigital.Api.Data;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Api.Repositories.Implementations;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasConProductosAsync(int restauranteId)
    {
        return await _context.Categorias
            .Include(c => c.Productos)
            .Where(c => c.RestauranteId == restauranteId)
            .ToListAsync();
    }

    public async Task<Categoria?> GetCategoriaByIdAsync(int categoriaId)
    {
        return await _context.Categorias.FindAsync(categoriaId);
    }

    public void AddCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void DeleteCategoria(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
    }

    public async Task<Producto?> GetProductoByIdAsync(int productoId)
    {
        return await _context.Productos.FindAsync(productoId);
    }

    public void AddProducto(Producto producto)
    {
        _context.Productos.Add(producto);
    }

    public void DeleteProducto(Producto producto)
    {
        _context.Productos.Remove(producto);
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasPorRestauranteAsync(int restauranteId)
    {
        return await _context.Categorias
            .Where(c => c.RestauranteId == restauranteId)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Producto>> GetProductosPorRestauranteAsync(int restauranteId)
    {
        return await _context.Productos
            .Include(p => p.Categoria) 
            .Where(p => p.Categoria.RestauranteId == restauranteId)
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }
}