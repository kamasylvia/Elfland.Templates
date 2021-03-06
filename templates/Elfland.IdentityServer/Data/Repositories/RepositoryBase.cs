using Microsoft.EntityFrameworkCore;

namespace Elfland.IdentityServer.Data.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task AddAsync(T item) => await _context.Set<T>().AddAsync(item);

    public virtual async Task AddRangeAsync(IEnumerable<T> items) =>
        await _context.Set<T>().AddRangeAsync(items);

    public virtual async Task AddRangeAsync(params T[] items) =>
        await _context.Set<T>().AddRangeAsync(items);

    public virtual async Task<bool> ExistsAsync(params object[] keyValues) =>
        (await FindByIdAsync(keyValues)) is null;

    public virtual async Task<IEnumerable<T>> FindAllAsync() =>
        await _context.Set<T>().ToListAsync();

    public virtual async Task<T> FindByIdAsync(params object[] keyValues) =>
        (await _context.Set<T>().FindAsync(keyValues))!;

    public virtual void Remove(T item) => _context.Set<T>().Remove(item);

    public virtual async Task<bool> SaveAsync() => await _context.SaveChangesAsync() >= 0;

    public virtual void Update(T item) => _context.Set<T>().Update(item);
}
