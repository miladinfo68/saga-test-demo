﻿using Shared.Base;
using Shared.Messages;

namespace Shared.Events;

public class StockReservedEvent : IDomainEvent
{
    public long OrderId { get; set; }
    public string CustomerId { get; set; }
    public PaymentMessage Payment { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; } = new();
}