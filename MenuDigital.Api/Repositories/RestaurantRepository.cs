using MenuDigital.Api.Data;
using MenuDigital.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Api.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await _context.Restaurants.FindAsync(id);
    }

    public async Task<Restaurant?> GetWithMenuAsync(int id)
    {
        return await _context.Restaurants
        .Include(r => r.Categories)
        .ThenInclude(c => c.Products)
        .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Restaurant?> GetByUserIdAsync(int userId)
    {
        return await _context.Restaurants
        .FirstOrDefaultAsync(r => r.UserId == userId);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

}