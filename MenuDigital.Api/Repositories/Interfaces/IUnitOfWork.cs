namespace MenuDigital.Api.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}