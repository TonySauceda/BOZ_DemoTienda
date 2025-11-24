using DemoTienda.Application.DTOs.Producto;
using DemoTienda.Domain.Entities;

namespace DemoTienda.Application.Extensions;

public static class ProductoExtensions
{
    extension(Producto producto)
    {
        public ProductoResponse ToDto()
        {
            return new ProductoResponse
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdCategoria = producto.IdCategoria,
                NombreCategoria = producto.IdCategoriaNavigation?.Nombre,
                EsActivo = producto.EsActivo,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }

    extension(IEnumerable<Producto> productos)
    {
        public IEnumerable<ProductoResponse> ToDto()
        {
            return productos.Select(producto => producto.ToDto());
        }
    }

    extension(CreateProductoRequest createProductoRequest)
    {
        public Producto ToEntity()
        {
            return new Producto
            {
                Nombre = createProductoRequest.Nombre,
                Descripcion = createProductoRequest.Descripcion,
                Precio = createProductoRequest.Precio,
                IdCategoria = createProductoRequest.IdCategoria,
                EsActivo = createProductoRequest.EsActivo,
                FechaCreacion = DateTime.UtcNow
            };
        }
    }

    extension(UpdateProductoRequest updateProductoRequest)
    {
        public void ToEntity(Producto producto)
        {
            producto.Nombre = updateProductoRequest.Nombre;
            producto.Descripcion = updateProductoRequest.Descripcion;
            producto.Precio = updateProductoRequest.Precio;
            producto.IdCategoria = updateProductoRequest.IdCategoria;
            producto.EsActivo = updateProductoRequest.EsActivo;
        }
    }
}
