using MenuDigital.Api.Entities;

namespace MenuDigital.Api.Repositories.Interfaces;

public interface IRestauranteRepository
{
   Task<IEnumerable<Restaurante>> GetRestaurantes();
   Task<Restaurante?> GetRestauranteByIdAsync(int id);
   
   Task<Restaurante?> GetRestauranteByEmail(string email);
   
   Task AddRestaurante(Restaurante restaurante);
   
   void DeleteRestaurante(Restaurante restaurante);
}