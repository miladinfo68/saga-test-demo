using MassTransit;
using MediatR;
using Order.Application.Orders.Commands.TestOrder;

namespace Order.API.Consumers;

public class TestOrderEventConsumer : IConsumer<TestOrder>
{
    private readonly IMediator _mediator;
 

    public TestOrderEventConsumer( IMediator mediator        )
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<TestOrder> context)
    {
        Console.WriteLine(context.Message.Name);
       await Task.CompletedTask;
    }
}

