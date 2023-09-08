using MediatR;
using Shared.Dtos;

namespace Stock.Application.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest
    {
        public long OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
