using Microsoft.EntityFrameworkCore;
using Shared.Base;

namespace Core.Infrastructure.Repositories;

public abstract class BaseRepository<T, TDbContext> : IRepository<T>
    where T : EntityBase
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    protected BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T item)
    {
        _dbContext.Set<T>().Add(item);
    }

    public async Task<T?> GetById(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
}

