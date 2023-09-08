using Core.Infrastructure.Repositories;
using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using Shared.Base;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Shared.Entities.Order , OrderDbContext> , IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}

