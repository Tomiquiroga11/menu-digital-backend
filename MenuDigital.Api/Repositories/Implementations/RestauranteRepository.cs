using MenuDigital.Api.Data;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Api.Repositories.Implementations;

public class RestauranteRepository : IRestauranteRepository
{
    private readonly ApplicationDbContext _context;

    public RestauranteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurante>> GetRestaurantes()
    {
        return await _context.Restaurantes.OrderBy(r => r.Nombre).ToListAsync();
    }

    public async Task<Restaurante?> GetRestauranteByIdAsync(int id)
    {
        return await _context.Restaurantes.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Restaurante?> GetRestauranteByEmail(string email)
    {
        return await _context.Restaurantes.FirstOrDefaultAsync(r => r.Email.ToLower() == email.ToLower());
    }

    public async Task AddRestaurante(Restaurante restaurante)
    {
        await _context.Restaurantes.AddAsync(restaurante);
    }
    
    public void DeleteRestaurante(Restaurante restaurante)
    {
        _context.Restaurantes.Remove(restaurante);
    }
}