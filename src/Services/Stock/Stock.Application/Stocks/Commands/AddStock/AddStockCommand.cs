﻿using MediatR;

namespace Stock.Application.Stocks.Commands.AddStock
{
    public class AddStockCommand : IRequest
    {
        // In Memory Database
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
