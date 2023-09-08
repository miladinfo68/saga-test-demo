using Core.Infrastructure.Repositories;
using Payment.Domain.Common;
using Payment.Infrastructure.Persistence;
using Shared.Base;

namespace Payment.Infrastructure.Repositories;


public class PaymentUnitOfWork : BaseUnitOfWork< PaymentDbContext>  , IPaymentUnitOfWork
{
    public PaymentUnitOfWork(PaymentDbContext dbContext) : base(dbContext)
    {
    }

    public IPaymentRepository PaymentRepository { get; set; }

}

