using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Events;
using Stock.Domain.Common;
using Core.Application.BusActions;
using Shared.Constants;

namespace Stock.Application.Stocks.Commands.ReduceStock
{
    public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand>
    {
        private readonly IMassTransitHandler _massTransitHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<ReduceStockCommandHandler> _logger;
        private readonly IStockUnitOfWork _unitOfWork;
        public ReduceStockCommandHandler(IMassTransitHandler massTransitHandler,
            IMapper mapper,
            ILogger<ReduceStockCommandHandler> logger,
            IStockUnitOfWork unitOfWork)
        {
            _massTransitHandler = massTransitHandler;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
            bool result = true;

            foreach (var orderItem in request.OrderItems)
            {
                if (!await _unitOfWork.StockRepository.AnyWithQuantity(orderItem.ProductId, orderItem.Quantity))
                {
                    result = false;
                    break;
                }
            }

            if (!result)
            {
                _logger.LogInformation($"OrderId: {request.OrderId} | Out of Stock");

                await _massTransitHandler.Publish(new StockNotReservedEvent
                {
                    OrderId = request.OrderId,
                    Message = "Out of Stock"
                },
                typeof(StockNotReservedEvent));

            }

            foreach (var orderItem in request.OrderItems)
            {
                var stock = await _unitOfWork.StockRepository.GetByProductId(productId: orderItem.ProductId);
                if (stock != null)
                    stock.Quantity -= orderItem.Quantity;
            }

            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"OrderId: {request.OrderId} | Stock Reserved");

            var @event = _mapper.Map<StockReservedEvent>(request);

            await _massTransitHandler.Send(RabbitMqConstants.StockReservedEventQueueName, @event, typeof(StockReservedEvent));
            return await Task.FromResult(Unit.Value);
        }
    }
}
