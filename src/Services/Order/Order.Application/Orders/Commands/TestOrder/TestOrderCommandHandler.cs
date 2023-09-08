using Core.Application.BusActions;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Common;
using Shared.Base;
using Shared.Constants;
using Shared.Events;

namespace Order.Application.Orders.Commands.TestOrder;

public class TestOrderCommandHandler : IRequestHandler<TestOrder>
{
    private readonly IOrderUnitOfWork _unitOfWork;
    private readonly IMassTransitHandler _massTransitHandler;
    private readonly ILogger<TestOrderCommandHandler> _logger;

    public TestOrderCommandHandler(
        IMassTransitHandler massTransitHandler,
        ILogger<TestOrderCommandHandler> logger)
    {
        _massTransitHandler = massTransitHandler;
        _logger = logger;
    }
    public async Task<Unit> Handle(TestOrder request, CancellationToken cancellationToken)
    {
        await _massTransitHandler.Publish(request, typeof(TestOrder));

        return Unit.Value;

    }
}

public class TestOrder:IDomainEvent ,IRequest
{
    public string Name { get; set; }
}