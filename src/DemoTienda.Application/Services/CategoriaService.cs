using DemoTienda.Application.DTOs;
using DemoTienda.Application.DTOs.Categoria;
using DemoTienda.Application.DTOs.Producto;
using DemoTienda.Application.Extensions;
using DemoTienda.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DemoTienda.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly ILogger<CategoriaService> _logger;

    public CategoriaService(ICategoriaRepository categoriaRepository, ILogger<CategoriaService> logger)
    {
        _categoriaRepository = categoriaRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoriaResponse>> ListAsync()
    {
        _logger.LogInformation("Listando categorias");

        var categories = await _categoriaRepository.ListAsync();

        return categories.ToDto();
    }

    public async Task<Result<CategoriaResponse>> GetByIdAsync(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);

        if (categoriaEntity is null)
            return Result<CategoriaResponse>.Failure("Id", $"Categoria {id} no encontrado", HttpStatusCode.NotFound);

        return categoriaEntity.ToDto();
    }

    public async Task<Result<CategoriaResponse>> AddAsync(CreateCategoriaRequest createCategoriaRequest)
    {
        var categoriaEntity = await _categoriaRepository.AddAsync(createCategoriaRequest.ToEntity());

        return categoriaEntity.ToDto();
    }

    public async Task<Result> UpdateAsync(int id, UpdateCategoriaRequest updateCategoriaRequest)
    {
        if (id != updateCategoriaRequest.Id)
            return Result.Failure("Id", "El id de la categoría no coincide con el id de la ruta", HttpStatusCode.BadRequest);

        var categoriaEntity = await _categoriaRepository.GetByIdAsync(updateCategoriaRequest.Id);

        if (categoriaEntity is null)
            return Result.Failure("Id", $"Categoria {id} no encontrada", HttpStatusCode.NotFound);

        updateCategoriaRequest.ToEntity(categoriaEntity);

        await _categoriaRepository.UpdateAsync(categoriaEntity);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);

        if (categoriaEntity is null)
            return Result.Failure("Id", $"Categoria {id} no encontrado", HttpStatusCode.NotFound);

        await _categoriaRepository.DeleteAsync(categoriaEntity);

        return Result.Success();
    }
}