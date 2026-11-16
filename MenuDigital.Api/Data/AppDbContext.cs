using MenuDigital.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Restaurant)
            .WithOne(r => r.User)
            .HasForeignKey<Restaurant>(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}