using MenuDigital.Api.Entities;
using MenuDigital.Api.Models;
using MenuDigital.Api.Repositories.Interfaces;
using MenuDigital.Api.Services.Interfaces;
using MenuDigital.Api.Mappers;

namespace MenuDigital.Api.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRestauranteRepository _restauranteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MenuService(IMenuRepository menuRepository, IRestauranteRepository restauranteRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _menuRepository = menuRepository;
            _restauranteRepository = restauranteRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MenuCompletoDto?> GetMenuCompletoAsync(int restauranteId)
        {
            var restaurante = await _restauranteRepository.GetRestauranteByIdAsync(restauranteId);
            if (restaurante == null)
            {
                return null;
            }

            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                restaurante.Visitas++;
                await _unitOfWork.SaveChangesAsync();
            }

            var categoriasConProductos = await _menuRepository.GetCategoriasConProductosAsync(restauranteId);

            var menuDto = new MenuCompletoDto
            {
                IdRestaurante = restauranteId,
                NombreRestaurante = restaurante.Nombre,
                Categorias = categoriasConProductos.Select(c => new CategoriaConProductosDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Productos = c.Productos.Select(p => p.ToDto()).ToList() // <-- Usa el Mapper
                }).ToList()
            };
            return menuDto;
        }

        public async Task<CategoriaDto?> CreateCategoriaAsync(CategoriaCreateDto dto, int restauranteId)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                RestauranteId = restauranteId,
            };
            _menuRepository.AddCategoria(categoria);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return categoria.ToDto(); 
            }
            return null;
        }

        public async Task<bool> UpdateCategoriaAsync(int categoriaId, CategoriaCreateDto dto, int restauranteId)
        {
            var categoria = await _menuRepository.GetCategoriaByIdAsync(categoriaId);
            
            if (categoria == null) return false;
            
            if(categoria.RestauranteId != restauranteId)
            {
                return false;
            }
            categoria.Nombre = dto.Nombre;
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoriaAsync(int categoriaId, int restauranteId)
        {
            var categoria = await _menuRepository.GetCategoriaByIdAsync(categoriaId);
            
            if (categoria == null) return false;
            
            if (categoria.RestauranteId != restauranteId) return false;
            
            _menuRepository.DeleteCategoria(categoria);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CategoriaDto?> GetCategoriaSimpleAsync(int categoriaId, int restauranteId)
        {
            var categoria = await _menuRepository.GetCategoriaByIdAsync(categoriaId);
            
            if (categoria == null) return null;

            if (categoria.RestauranteId != restauranteId)
            {
                return null;
            }
            return categoria.ToDto(); 
        }

        public async Task<IEnumerable<CategoriaDto>> GetMisCategoriasAsync(int restauranteId)
        {
            var categorias = await _menuRepository.GetCategoriasPorRestauranteAsync(restauranteId);
            
            return categorias.Select(c => c.ToDto());
        }

        public async Task<ProductoDto?> CreateProductoAsync(ProductoCreateDto dto, int restauranteId)
        {
            var categoria = await GetCategoriaSimpleAsync(dto.CategoriaId, restauranteId);
            if (categoria == null)
            {
                return null; 
            }

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                EstaDestacado = dto.EstaDestacado,
                CategoriaId = dto.CategoriaId,
                ImagenUrl = dto.ImagenUrl,
                TieneDescuento = dto.TieneDescuento,
                PorcentajeDescuento = dto.PorcentajeDescuento,
                TieneHappyHour = dto.TieneHappyHour,
            };
            _menuRepository.AddProducto(producto);
            await _unitOfWork.SaveChangesAsync();
            
            return producto.ToDto(); 
        }

        public async Task<bool> UpdateProductoAsync(int productoId, ProductoUpdateDto dto, int restauranteId)
        {
            var categoriaDestino = await _menuRepository.GetCategoriaByIdAsync(dto.CategoriaId);
            if (categoriaDestino == null || categoriaDestino.RestauranteId != restauranteId)
            {
                return false; 
            }
            
            var producto = await _menuRepository.GetProductoByIdAsync(productoId);
            if (producto == null) return false;

            var categoriaActual = await _menuRepository.GetCategoriaByIdAsync(producto.CategoriaId);
            if (categoriaActual == null || categoriaActual.RestauranteId != restauranteId)
            {
                return false; 
            }
            
            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.EstaDestacado = dto.EstaDestacado;
            producto.CategoriaId = dto.CategoriaId;
            producto.ImagenUrl = dto.ImagenUrl;
            producto.TieneDescuento = dto.TieneDescuento;
            producto.PorcentajeDescuento = dto.PorcentajeDescuento;
            producto.TieneHappyHour = dto.TieneHappyHour;
            
            return await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<bool> DeleteProductoAsync(int productoId, int restauranteId)
        {
            var producto = await _menuRepository.GetProductoByIdAsync(productoId);
            if (producto == null) return false;

            var categoriaActual = await _menuRepository.GetCategoriaByIdAsync(producto.CategoriaId);
            if (categoriaActual == null || categoriaActual.RestauranteId != restauranteId)
            {
                return false; 
            }

            _menuRepository.DeleteProducto(producto);
            return await _unitOfWork.SaveChangesAsync();
        }

        private async Task<Producto?> GetProductoSiEsDueño(int productoId, int restauranteId)
        {
            var producto = await _menuRepository.GetProductoByIdAsync(productoId);
            if (producto == null) return null;

            var categoria = await _menuRepository.GetCategoriaByIdAsync(producto.CategoriaId);
            if (categoria == null || categoria.RestauranteId != restauranteId)
            {
                return null; 
            }
            return producto; 
        }

        public async Task<bool> SetDescuentoAsync(int productoId, ProductoDescuentoDto dto, int restauranteId)
        {
            var producto = await GetProductoSiEsDueño(productoId, restauranteId);
            if (producto == null)
            {
                return false; 
            }

            producto.TieneDescuento = dto.TieneDescuento;
            producto.PorcentajeDescuento = dto.TieneDescuento ? dto.PorcentajeDescuento : 0; 

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SetHappyHourAsync(int productoId, ProductoHappyHourDto dto, int restauranteId)
        {
            var producto = await GetProductoSiEsDueño(productoId, restauranteId);
            if (producto == null)
            {
                return false; 
            }

            producto.TieneHappyHour = dto.TieneHappyHour;
            return await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<ProductoDto>> GetMisProductosAsync(int restauranteId)
        {
            var productos = await _menuRepository.GetProductosPorRestauranteAsync(restauranteId);
            return productos.Select(p => p.ToDto());
        }
    }
}