using MassTransit;
using MediatR;
using Order.Application.Orders.Commands.UpdateOrderStatus;
using Shared.Enums;
using Shared.Events;

namespace Order.API.Consumers;

public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
{
    private readonly IMediator _mediator;

    public StockNotReservedEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        await _mediator.Send(new UpdateOrderStatusCommand
        {
            OrderId = context.Message.OrderId,
            Status = OrderStatus.Failed,
            FailMessage = context.Message.Message
        });
    }
}