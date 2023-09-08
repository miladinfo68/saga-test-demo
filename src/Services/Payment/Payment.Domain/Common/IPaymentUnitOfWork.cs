using Shared.Base;

namespace Payment.Domain.Common;

public interface IPaymentUnitOfWork : IUnitOfWork
{
    IPaymentRepository PaymentRepository { get; }
}