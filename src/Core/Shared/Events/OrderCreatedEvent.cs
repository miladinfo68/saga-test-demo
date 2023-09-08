using Shared.Base;
using Shared.Dtos;
using Shared.Enums;
using Shared.Messages;

namespace Shared.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public long OrderId { get; set; }
    public string CustomerId { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; } = new();
    public PaymentMessage Payment { get; set; }

    public OrderCreatedEvent(long orderId, string customerId, PaymentMessage payment)
    {
       OrderId = orderId;
       CustomerId = customerId;
       Payment = payment;
    }

    public void AddOrderItem(OrderItemMessage orderItem) => OrderItems.Add(orderItem);
}

public class OrderCreatedEventV2 : IDomainEvent
{
    public OrderCreatedEventV2(
        double orderId,
        OrderStatus status, 
        decimal total, 
        DateTime orderDate, 
        OrderDto orderDto)
    {
        OrderId = orderId;
        Status = status;
        Total = total;
        OrderDate = orderDate;
        OrderDto = orderDto;
    }

    public double OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public  DateTime OrderDate { get; set; }
    public OrderDto OrderDto { get; set; }

    public static implicit operator OrderCreatedEventV2(CreatedOrderDto dto) =>
         new OrderCreatedEventV2(dto.OrderId, dto.Status, dto.Total,dto.OrderDate ,dto.OrderDto);

}