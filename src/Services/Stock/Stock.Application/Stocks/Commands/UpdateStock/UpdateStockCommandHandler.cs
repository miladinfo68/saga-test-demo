using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain.Common;

namespace Stock.Application.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand>
    {
        private readonly IStockUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateStockCommandHandler> _logger;
        public UpdateStockCommandHandler(IStockUnitOfWork unitOfWork,
            ILogger<UpdateStockCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderItems == null || request.OrderItems.Count == 0)
                 await Task.CompletedTask;

            foreach (var orderItem in request.OrderItems)
            {
                var product = await _unitOfWork.StockRepository.GetByProductId(orderItem.ProductId);

                _logger.LogInformation($"ProductId: {product.ProductId} | {orderItem.Quantity} item(s) Added");
                product.Quantity += orderItem.Quantity;
            }

            _logger.LogInformation($"OrderId: {request.OrderId} | Order Canceled");

            await _unitOfWork.CommitAsync();
            return await Task.FromResult(Unit.Value);

        }
    }
}
