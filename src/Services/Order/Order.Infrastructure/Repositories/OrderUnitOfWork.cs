using Core.Infrastructure.Repositories;
using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using Shared.Base;

namespace Order.Infrastructure.Repositories;

public class OrderUnitOfWork : BaseUnitOfWork< OrderDbContext> , IOrderUnitOfWork
{
    public OrderUnitOfWork(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public IOrderRepository OrderRepository { get; }
}