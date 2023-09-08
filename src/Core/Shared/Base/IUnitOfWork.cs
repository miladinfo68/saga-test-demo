namespace Shared.Base;

public interface IUnitOfWork :IDisposable
{
    void Commit();
    void Rollback();
    Task CommitAsync();
    Task RollbackAsync();
}