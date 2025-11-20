using DemoTienda.Domain.Entities;

namespace DemoTienda.Mocks.Mocks;

public class CategoriaMock
{
    public static List<Categoria> DataSet => [
                new Categoria { Id = 1, Nombre = "Categoria 1", Descripcion = "Descripcion Categoria 1", EsActiva = true, FechaCreacion = DateTime.UtcNow },
                new Categoria { Id = 2, Nombre = "Categoria 2", Descripcion = "Descripcion Categoria 2", EsActiva = true, FechaCreacion = DateTime.UtcNow },
                new Categoria { Id = 3, Nombre = "Categoria 3", Descripcion = "Descripcion Categoria 3", EsActiva = false, FechaCreacion = DateTime.UtcNow },
                new Categoria { Id = 4, Nombre = "Categoria 4", Descripcion = "Descripcion Categoria 4", EsActiva = true, FechaCreacion = DateTime.UtcNow },
                new Categoria { Id = 5, Nombre = "Categoria 5", Descripcion = "Descripcion Categoria 5", EsActiva = true, FechaCreacion = DateTime.UtcNow }
            ];
}