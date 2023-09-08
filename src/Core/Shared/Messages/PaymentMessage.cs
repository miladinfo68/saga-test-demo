namespace Shared.Messages;

/*public class PaymentMessage
{
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string Cvv { get; set; }
    public decimal TotalPrice { get; set; }

    public PaymentMessage(string cardName, string cardNumber, string expiration, string cvv, decimal totalPrice)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        TotalPrice = totalPrice;
    }
}*/


public record PaymentMessage(
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