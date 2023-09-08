using Core.Infrastructure.Repositories;
using Payment.Domain.Common;
using Payment.Infrastructure.Persistence;
using Shared.Base;

namespace Payment.Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Shared.Entities.Payment, PaymentDbContext> , IPaymentRepository
{
    public PaymentRepository(PaymentDbContext dbContext) : base(dbContext)
    {
    }
}