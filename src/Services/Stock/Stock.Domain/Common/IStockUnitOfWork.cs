using Shared.Base;
using Stock.Domain.Common;

namespace Stock.Domain.Common;

public interface IStockUnitOfWork : IUnitOfWork
{
    IStockRepository StockRepository { get; }
}