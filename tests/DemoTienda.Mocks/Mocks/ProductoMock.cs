using DemoTienda.Domain.Entities;

namespace DemoTienda.Mocks.Mocks;

public class ProductoMock
{
    public static List<Producto> DataSet => [
                new Producto { Id = 1, Nombre = "Producto 1", Descripcion = "Descripcion Producto 1", Precio = 10.0m, IdCategoria = 2, EsActivo = true, FechaCreacion = DateTime.UtcNow },
                new Producto { Id = 2, Nombre = "Producto 2", Descripcion = "Descripcion Producto 2", Precio = 20.0m, IdCategoria = 2, EsActivo = true, FechaCreacion = DateTime.UtcNow },
                new Producto { Id = 3, Nombre = "Producto 3", Descripcion = "Descripcion Producto 3", Precio = 30.0m, IdCategoria = 4, EsActivo = true, FechaCreacion = DateTime.UtcNow },
                new Producto { Id = 4, Nombre = "Producto 4", Descripcion = "Descripcion Producto 4", Precio = 40.0m, IdCategoria = 4, EsActivo = true, FechaCreacion = DateTime.UtcNow },
                new Producto { Id = 5, Nombre = "Producto 5", Descripcion = "Descripcion Producto 5", Precio = 50.0m, IdCategoria = 5, EsActivo = true, FechaCreacion = DateTime.UtcNow },
                new Producto { Id = 6, Nombre = "Producto 6", Descripcion = "Descripcion Producto 6", Precio = 60.0m, IdCategoria = 5, EsActivo = true, FechaCreacion = DateTime.UtcNow },
            ];
}
