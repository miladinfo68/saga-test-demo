using Core.Application.BusActions;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Common;
using Shared.Dtos;
using Shared.Entities;
using Shared.Events;

namespace Order.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderUnitOfWork _unitOfWork;
    private readonly IMassTransitHandler _massTransitHandler;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderUnitOfWork unitOfWork,
        IMassTransitHandler massTransitHandler,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _massTransitHandler = massTransitHandler;
        _logger = logger;
    }


    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Shared.Entities.Order.Build(order =>
        {
            order.WithCustomerId(request.CustomerId);
            order.WithAddress(request?.AddressDto);
            order.WithOrderItems((List<OrderItem>)
                request.OrderItems
                .Select(s => OrderItem
                .CreateOrderItem(order.Id, s.ProductId, s.Quantity, s.Price))
                );
        });

        _unitOfWork.OrderRepository.Add(order);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation($"OrderId: {order.Id} | Order Created");

        OrderCreatedEventV2 orderCreatedEvent = new CreatedOrderDto(order.Id,order.Status,order.Total,order.OrderDate , request);

        await _massTransitHandler.Publish(orderCreatedEvent, typeof(OrderCreatedEventV2));

        return Unit.Value;
    }
}