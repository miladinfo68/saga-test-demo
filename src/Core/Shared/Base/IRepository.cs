namespace Shared.Base;

public interface IRepository<T> where T : EntityBase
{
    void Add(T item);

    Task<T?> GetById(long id);
}