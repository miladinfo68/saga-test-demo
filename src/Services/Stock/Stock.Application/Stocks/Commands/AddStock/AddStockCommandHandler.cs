using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain.Common;

namespace Stock.Application.Stocks.Commands.AddStock
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand>
    {
        private readonly IStockUnitOfWork _unitOfWork;
        private readonly ILogger<AddStockCommandHandler> _logger;

        public AddStockCommandHandler(IStockUnitOfWork unitOfWork,
            ILogger<AddStockCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _unitOfWork.StockRepository.GetByProductId(request.ProductId);

            if (stock == null)
            {
                stock = new Domain.Entities.Stock
                {
                    Id = request.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                //await _unitOfWork.StockRepository.Add(stock);

                _logger.LogInformation($"Stock created. Id: {stock.Id} Quantity: {stock.Quantity}");
            }
            else
            {
                stock.Quantity += request.Quantity;

                _logger.LogInformation($"Stock added. Id: {stock.Id} Quantity: {stock.Quantity}");
            }

            //await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
