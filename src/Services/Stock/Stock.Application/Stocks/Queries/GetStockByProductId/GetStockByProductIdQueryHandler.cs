using AutoMapper;
using MediatR;
using Shared.Dtos;
using Stock.Domain.Common;

namespace Stock.Application.Stocks.Queries.GetStockByProductId
{
    public class GetStockByProductIdQueryHandler : IRequestHandler<GetStockByProductIdQuery, StockDto>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public GetStockByProductIdQueryHandler(IStockRepository stockRepository,
            IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<StockDto> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductId(request.ProductId);

            if (stock == null)
                throw new Exception("Stock not found.");

            return _mapper.Map<StockDto>(stock);
        }
    }
}
