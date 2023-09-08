namespace Shared.Messages;

public class OrderItemMessage
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderItemMessage(long productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}