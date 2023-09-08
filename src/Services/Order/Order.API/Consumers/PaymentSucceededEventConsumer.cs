using MassTransit;
using MediatR;
using Order.Application.Orders.Commands.UpdateOrderStatus;
using Shared.Enums;
using Shared.Events;

namespace Order.API.Consumers;

public class PaymentSucceededEventConsumer : IConsumer<PaymentSucceededEvent>
{
    private readonly IMediator _mediator;

    public PaymentSucceededEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentSucceededEvent> context)
    {
        await _mediator.Send(new UpdateOrderStatusCommand
        {
            OrderId = context.Message.OrderId,
            Status = OrderStatus.Success
        });
    }
}