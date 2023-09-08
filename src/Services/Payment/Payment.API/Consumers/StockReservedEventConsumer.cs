using AutoMapper;
using MassTransit;
using MediatR;
using Payment.Application.Payments.Commands.CreatePaymentProcess;
using Shared.Events;

namespace Payment.API.Consumers;

public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StockReservedEventConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        await _mediator.Send(_mapper.Map<CreatePaymentProcessCommand>(context.Message));
    }
}