using MediatR;
using Shared.Dtos;

namespace Order.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest
{
    public string CustomerId { get; set; } = null!;
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = null!;
    public AddressDto? AddressDto { get; set; }

    public static implicit operator OrderDto(CreateOrderCommand command)
    {
        return new OrderDto(
            command.CustomerId,
            command.OrderItems,
            command.AddressDto);
    }
}
