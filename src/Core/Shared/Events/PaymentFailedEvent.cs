using Shared.Base;
using Shared.Messages;

namespace Shared.Events;

public class PaymentFailedEvent : IDomainEvent
{
    public long OrderId { get; set; }
    public string CustomerId { get; set; }
    public string Message { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; } = new();
}