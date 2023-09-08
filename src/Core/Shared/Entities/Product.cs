using Shared.Base;
using Shared.Dtos;
using Shared.Enums;

namespace Shared.Entities;
public class Product : EntityBase
{
    public Product(long id, string name, int quantity, decimal price)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Order : EntityBase
{
    public  string  CustomerId { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public decimal Total { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Address? Address { get; private set; }
    public string? Message { get; private set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    private Order() { }

    public Order WithCustomerId(string customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Waiting;
        OrderDate = DateTime.UtcNow;
        return this;
    }

    public Order WithOrderItems(List<OrderItem> orderItems)
    {
        OrderItems = orderItems;
        Total = orderItems.Sum(s => s.Quantity * s.Price);
        return this;
    }

    public Order WithAddress(Address? address)
    {
        if (address != null)
            Address = address;
        return this;
    }

    public Order WithMessage(string? message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Message = message;
        return this;
    }

    public static Order Build(Action<Order> buildAction)
    {
        var order = new Order();
        buildAction(order);
        return order;
    }


}

/*var order = Order.Build(order =>
{
    order.WithCustomerId("123");
    order.WithMessage("This is a test order");
    order.WithAddress(new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" });
    order.WithOrderItems(new List<OrderItem> { new OrderItem { ProductId = "456", Quantity = 2, Price = 25.00m } });
});*/

public class OrderItem : EntityBase
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public virtual Order Order { set; get; }
    public virtual Product Product { get; set; }

    private OrderItem(long orderId, long productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem CreateOrderItem(long orderId, long productId, int quantity, decimal price)
    {
        return new OrderItem(orderId, productId, quantity, price);
    }
}


public class Address
{
    public string Line { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
    public string PhoneNo { get; set; }

    private Address() { }

    private Address(string line, string province, string district, string phoneNo)
    {
        Line = line;
        Province = province;
        District = district;
        PhoneNo = phoneNo;
    }

    public static Address CreateAddress(string line, string province, string district, string phoneNo)
    {
        return new Address(line, province, district, phoneNo);
    }


}


public class Payment : EntityBase
{
    public Payment()
    {
    }

    public Payment(
        double orderId,
        string customerId,
        string orderItemIds,
        string cardName,
        string cardNumber,
        string expiration,
        string cvv,
        decimal totalPrice)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderItemIds = orderItemIds;
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        TotalPrice = totalPrice;
        PaymentDate = DateTime.UtcNow;
    }

    public double OrderId { get; set; }
    public string CustomerId { get; set; }
    public string OrderItemIds { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string Cvv { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PaymentDate { get; set; }
}