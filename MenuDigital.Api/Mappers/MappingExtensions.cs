using MenuDigital.Api.Entities;
using MenuDigital.Api.Models;

namespace MenuDigital.Api.Mappers;

public static class MappingExtensions
{
    public static CategoriaDto ToDto(this Categoria categoria)
    {
        return new CategoriaDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
        };
    }

    public static ProductoDto ToDto(this Producto producto, bool esHappyHourActivo = false)
    {
        bool aplicarHappyHour = producto.TieneHappyHour && esHappyHourActivo;

        decimal precioFinal = producto.Precio;
        decimal? precioOriginal = null;

        if (aplicarHappyHour)
        {
            precioOriginal = producto.Precio;
            precioFinal = producto.Precio * 0.5m; // 50% OFF
        }
        else if (producto.TieneDescuento)
        {
            precioOriginal = producto.Precio;
            precioFinal = producto.Precio - (producto.Precio * (producto.PorcentajeDescuento / 100m));
        }

        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
        
            Precio = Math.Round(precioFinal, 2), 
            PrecioOriginal = precioOriginal,
        
            EstaDestacado = producto.EstaDestacado,
            TieneDescuento = producto.TieneDescuento,
            PorcentajeDescuento = producto.PorcentajeDescuento,
            TieneHappyHour = producto.TieneHappyHour,
            CategoriaId = producto.CategoriaId,
            ImagenUrl = producto.ImagenUrl,
            
            EnHappyHourAhora = aplicarHappyHour 
        };
    }
}