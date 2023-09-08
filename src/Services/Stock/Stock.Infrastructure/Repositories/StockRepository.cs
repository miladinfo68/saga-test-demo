using Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;

namespace Stock.Infrastructure.Repositories
{
    public class StockRepository : BaseRepository<Domain.Entities.Stock , StockDbContext>, IStockRepository
    {
        private readonly StockDbContext _dbContext;
        public StockRepository(StockDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyWithQuantity(long productId, int quantity)
        {
            var result = await _dbContext.Stocks.AnyAsync(x => x.ProductId == productId && x.Quantity >= quantity);
            return result;
        }

        public async Task<Domain.Entities.Stock> GetByProductId(long productId)
        {
            return await _dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}
