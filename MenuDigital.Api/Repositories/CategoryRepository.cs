using MenuDigital.Api.Data;
using MenuDigital.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Api.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;


public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Categories
            .Where(c => c.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}