using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.Events;
using Core.Application.BusActions;
using MediatR;

namespace Payment.Application.Payments.Commands.CreatePaymentProcess;

public class CreatePaymentProcessCommandHandler : IRequestHandler<CreatePaymentProcessCommand>
{
    private readonly ILogger<CreatePaymentProcessCommandHandler> _logger;
    private readonly IMassTransitHandler _massTransitHandler;
    private readonly IMapper _mapper;

    public CreatePaymentProcessCommandHandler(ILogger<CreatePaymentProcessCommandHandler> logger, 
        IMassTransitHandler massTransitHandler,
        IMapper mapper)
    {
        _logger = logger;
        _massTransitHandler = massTransitHandler;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreatePaymentProcessCommand request, CancellationToken cancellationToken)
    {
        //var balance = 100;

        //if (balance > request.Payment.TotalPrice)
        //{
        //    _logger.LogInformation($"OrderId: {request.OrderId} | {request.Payment.TotalPrice} USD was Withdrawn");

        //    await _massTransitHandler.Publish(_mapper.Map<PaymentSucceededEvent>(request), typeof(PaymentSucceededEvent));
        //}
        //else
        //{
        //    _logger.LogInformation($"OrderId: {request.OrderId} | Insufficient Balance | {request.Payment.TotalPrice} USD");

        //    var @event = _mapper.Map<PaymentFailedEvent>(request);
        //    @event.Message = "Insufficient Balance";

        //    await _massTransitHandler.Publish(@event, typeof(PaymentFailedEvent));
        //}

        return await Task.FromResult(Unit.Value);
    }

   
}