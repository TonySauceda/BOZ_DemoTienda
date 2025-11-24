using DemoTienda.Application.DTOs;
using DemoTienda.Application.DTOs.Producto;
using DemoTienda.Application.Extensions;
using DemoTienda.Application.Interfaces;
using System.Net;

namespace DemoTienda.Application.Services;

public class ProductoService
{
    private readonly IProductoRepository _productoRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public ProductoService(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository)
    {
        _productoRepository = productoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<ProductoResponse>> ListAsync()
    {
        var productos = await _productoRepository.ListAsync();

        return productos.ToDto();
    }

    public async Task<Result<ProductoResponse>> GetByIdAsync(int id)
    {
        var productoEntity = await _productoRepository.GetByIdAsync(id);

        if (productoEntity is null)
            return Result<ProductoResponse>.Failure("Id", $"Producto {id} no encontrado", HttpStatusCode.NotFound);

        return productoEntity.ToDto();
    }

    public async Task<Result<ProductoResponse>> AddAsync(CreateProductoRequest createProductoRequest)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(createProductoRequest.IdCategoria);
        if (categoriaEntity is null)
            return Result<ProductoResponse>.Failure("IdCategoria", $"Categoria {createProductoRequest.IdCategoria} no encontrada", HttpStatusCode.BadRequest);

        var productoEntity = await _productoRepository.AddAsync(createProductoRequest.ToEntity());

        return productoEntity.ToDto();
    }

    public async Task<Result> UpdateAsync(int id, UpdateProductoRequest updateProductoRequest)
    {
        if (id != updateProductoRequest.Id)
            return Result.Failure("Id", "El id del producto no coincide con el id de la ruta", HttpStatusCode.BadRequest);

        var productoEntity = await _productoRepository.GetByIdAsync(id);

        if (productoEntity is null)
            return Result.Failure("Id", $"Producto {id} no encontrado", HttpStatusCode.NotFound);

        var categoriaEntity = await _categoriaRepository.GetByIdAsync(updateProductoRequest.IdCategoria);
        if (categoriaEntity is null)
            return Result.Failure("IdCategoria", $"Categoria {id} no encontrada", HttpStatusCode.BadRequest);

        updateProductoRequest.ToEntity(productoEntity);

        await _productoRepository.UpdateAsync(productoEntity);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var productoEntity = await _productoRepository.GetByIdAsync(id);

        if (productoEntity is null)
            return Result.Failure("Id", $"Producto {id} no encontrado", HttpStatusCode.NotFound);

        await _productoRepository.DeleteAsync(productoEntity);

        return Result.Success();
    }
}