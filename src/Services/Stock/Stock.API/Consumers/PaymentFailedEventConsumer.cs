using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events;
using Stock.Application.Stocks.Commands.UpdateStock;

namespace Stock.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentFailedEventConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            await _mediator.Send(_mapper.Map<UpdateStockCommand>(context.Message));
        }
    }
}
