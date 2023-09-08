using Core.Application.BusActions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Application.Orders.Commands.TestOrder;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    //[HttpPost("SendMessage")]
    //public async Task<IActionResult> SendMessage(TestOrder order)
    //{
    //    await _mediator.Send(order);
    //    //await _publishEndpoint.Publish<TestOrder>(order);

    //    return Ok();
    //}


    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    
}