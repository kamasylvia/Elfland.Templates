namespace Elfland.WebApi.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T item);

    Task AddRangeAsync(IEnumerable<T> items);

    Task AddRangeAsync(params T[] items);

    void Remove(T item);

    void Update(T item);

    Task<IEnumerable<T>> FindAllAsync();

    Task<T> FindByIdAsync(params object[] keyValues);

    Task<bool> ExistsAsync(params object[] keyValues);
}
