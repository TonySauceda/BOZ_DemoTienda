using DemoTienda.Application.Interfaces;
using DemoTienda.Domain.Entities;

namespace DemoTienda.Application.Services;

public class ProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<IEnumerable<Producto>> ListAsync() => _productoRepository.ListAsync();
    public Task<Producto?> GetByIdAsync(int id) => _productoRepository.GetByIdAsync(id);
    public Task<Producto> AddAsync(Producto producto) => _productoRepository.AddAsync(producto);
    public async Task UpdateAsync(Producto producto)
    {
        var productoEntity = await _productoRepository.GetByIdAsync(producto.Id);
        if (productoEntity is null)
            return;//Handle

        productoEntity.Nombre = producto.Nombre;
        productoEntity.Descripcion = producto.Descripcion;
        productoEntity.Precio = producto.Precio;
        productoEntity.IdCategoria = producto.IdCategoria;
        productoEntity.EsActivo = producto.EsActivo;

        await _productoRepository.UpdateAsync(productoEntity);
    }

    public async Task DeleteAsync(int id)
    {
        var productoEntity = await _productoRepository.GetByIdAsync(id);
        if (productoEntity is null)
            return;//Handle

        await _productoRepository.DeleteAsync(productoEntity);
    }
}
