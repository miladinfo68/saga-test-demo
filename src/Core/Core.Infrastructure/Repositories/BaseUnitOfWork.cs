using Microsoft.EntityFrameworkCore;
using Shared.Base;

namespace Core.Infrastructure.Repositories;

public abstract class BaseUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private bool disposed = false;

    private readonly TDbContext _dbContext;

    protected BaseUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Commit() => _dbContext.SaveChanges();
    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
    public async Task RollbackAsync()
    {
        Rollback();
        await Task.CompletedTask;
    }
    public void Rollback()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _dbContext.Dispose();
        }
        disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

