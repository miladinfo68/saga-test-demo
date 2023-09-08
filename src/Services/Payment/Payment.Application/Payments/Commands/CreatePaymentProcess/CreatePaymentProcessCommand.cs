using MediatR;
using Shared.Dtos;

namespace Payment.Application.Payments.Commands.CreatePaymentProcess;

public class CreatePaymentProcessCommand : IRequest
{
    //public long OrderId { get; set; }
    //public string CustomerId { get; set; }
    //public PaymentDto Payment { get; set; }
    //public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    public record PaymentDto(
    double OrderId,
    string CustomerId,
    string OrderItemIds,
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    decimal TotalPrice,
    DateTime PaymentDate
    );
}

