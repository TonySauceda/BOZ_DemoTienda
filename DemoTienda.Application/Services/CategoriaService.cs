using DemoTienda.Application.Interfaces;
using DemoTienda.Domain.Entities;

namespace DemoTienda.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public Task<IEnumerable<Categoria>> ListAsync() => _categoriaRepository.ListAsync();
    public Task<Categoria?> GetByIdAsync(int id) => _categoriaRepository.GetByIdAsync(id);
    public Task<Categoria> AddAsync(Categoria categoria) => _categoriaRepository.AddAsync(categoria);
    public async Task UpdateAsync(Categoria categoria)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(categoria.Id);
        if (categoriaEntity is null)
            return;//Handle

        categoriaEntity.Nombre = categoria.Nombre;
        categoriaEntity.Descripcion = categoria.Descripcion;
        categoriaEntity.EsActiva = categoria.EsActiva;

        await _categoriaRepository.UpdateAsync(categoriaEntity);
    }

    public async Task DeleteAsync(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
        if (categoriaEntity is null)
            return;//Handle

        await _categoriaRepository.DeleteAsync(categoriaEntity);
    }
}