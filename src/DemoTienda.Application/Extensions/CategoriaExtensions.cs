using DemoTienda.Application.DTOs.Categoria;
using DemoTienda.Domain.Entities;

namespace DemoTienda.Application.Extensions;

public static class CategoriaExtensions
{
    extension(Categoria categoria)
    {
        public CategoriaResponse ToDto()
        {
            return new CategoriaResponse
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                EsActiva = categoria.EsActiva,
                FechaCreacion = categoria.FechaCreacion
            };
        }
    }

    extension(IEnumerable<Categoria> categorias)
    {
        public IEnumerable<CategoriaResponse> ToDto()
        {
            return categorias.Select(categoria => categoria.ToDto());
        }
    }

    extension(CreateCategoriaRequest createCategoriaRequest)
    {
        public Categoria ToEntity()
        {
            return new Categoria
            {
                Nombre = createCategoriaRequest.Nombre,
                Descripcion = createCategoriaRequest.Descripcion,
                EsActiva = createCategoriaRequest.EsActiva,
                FechaCreacion = DateTime.UtcNow
            };
        }
    }

    extension(UpdateCategoriaRequest updateCategoriaRequest)
    {
        public void ToEntity(Categoria categoria)
        {
            categoria.Nombre = updateCategoriaRequest.Nombre;
            categoria.Descripcion = updateCategoriaRequest.Descripcion;
            categoria.EsActiva = updateCategoriaRequest.EsActiva;
        }
    }
}
