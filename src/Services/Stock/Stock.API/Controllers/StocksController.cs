using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Stocks.Commands.AddStock;
using Stock.Application.Stocks.Queries.GetStockByProductId;

namespace Stock.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddStock(AddStockCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetStockByProductId([FromQuery] GetStockByProductIdQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}