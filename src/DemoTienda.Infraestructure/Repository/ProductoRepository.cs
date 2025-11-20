using DemoTienda.Application.Interfaces;
using DemoTienda.Domain.Entities;
using DemoTienda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoTienda.Infraestructure.Repository;

public class ProductoRepository : IProductoRepository
{
    private readonly DemoTiendaContext _dbContext;

    public ProductoRepository(DemoTiendaContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Producto> AddAsync(Producto entity)
    {
        entity.FechaCreacion = DateTime.UtcNow;

        _dbContext.Productos.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Producto entity)
    {
        _dbContext.Productos.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _dbContext.Productos
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Producto>> ListAsync()
    {
        return await _dbContext.Productos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(Producto entity)
    {
        _dbContext.Productos.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
