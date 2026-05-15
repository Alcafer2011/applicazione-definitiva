using System.Linq.Expressions;

namespace CRM.Service.Infrastructure;

public interface IMongoRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(string id);
}