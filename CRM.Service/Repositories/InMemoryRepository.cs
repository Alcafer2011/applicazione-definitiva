using CRM.Service.Models;

namespace CRM.Service.Repositories;

public class InMemoryRepository<T> where T : class
{
    private readonly List<T> _items = new();

    public Task<List<T>> GetAllAsync() => Task.FromResult(_items.ToList());

    public Task<T?> GetByIdAsync(string id)
    {
        var prop = typeof(T).GetProperty("Id");
        return Task.FromResult(_items.FirstOrDefault(x => prop!.GetValue(x)!.ToString() == id));
    }

    public Task AddAsync(T item)
    {
        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(T item)
    {
        var prop = typeof(T).GetProperty("Id");
        var id = prop!.GetValue(item)!.ToString();
        var existing = _items.FirstOrDefault(x => prop.GetValue(x)!.ToString() == id);
        if (existing != null)
        {
            _items.Remove(existing);
            _items.Add(item);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string id)
    {
        var prop = typeof(T).GetProperty("Id");
        var existing = _items.FirstOrDefault(x => prop!.GetValue(x)!.ToString() == id);
        if (existing != null)
            _items.Remove(existing);

        return Task.CompletedTask;
    }
}