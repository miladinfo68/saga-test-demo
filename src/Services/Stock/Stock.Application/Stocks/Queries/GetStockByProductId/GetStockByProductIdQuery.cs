using MediatR;
using Shared.Dtos;

namespace Stock.Application.Stocks.Queries.GetStockByProductId
{
    public class GetStockByProductIdQuery : IRequest<StockDto>
    {
        public long ProductId { get; set; }
    }
}
