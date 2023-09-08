using Core.Infrastructure.Repositories;
using Shared.Base;
using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;

namespace Stock.Infrastructure.UnitOfWork
{
    public class StockUnitOfWork : BaseUnitOfWork< StockDbContext>, IStockUnitOfWork
    {
        public StockUnitOfWork(StockDbContext dbContext) : base(dbContext)
        {
        }

        public IStockRepository StockRepository { get; }
    }
}
