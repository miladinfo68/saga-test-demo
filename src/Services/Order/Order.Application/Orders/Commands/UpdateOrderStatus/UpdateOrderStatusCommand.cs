using MediatR;
using Shared.Enums;

namespace Order.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommand : IRequest
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public string FailMessage { get; set; }
}