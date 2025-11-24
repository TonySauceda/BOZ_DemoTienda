using DemoTienda.Application.Interfaces;
using DemoTienda.Domain.Entities;
using DemoTienda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoTienda.Infraestructure.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly DemoTiendaContext _dbContext;

    public CategoriaRepository(DemoTiendaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Categoria> AddAsync(Categoria entity)
    {
        _dbContext.Categorias.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Categoria entity)
    {
        _dbContext.Categorias.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _dbContext.Categorias
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Categoria>> ListAsync()
    {
        return await _dbContext.Categorias
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(Categoria entity)
    {
        _dbContext.Categorias.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
