namespace DemoTienda.Application.Interfaces;

public interface ICRUDRepository<T>
{
    Task<IEnumerable<T>> ListAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}