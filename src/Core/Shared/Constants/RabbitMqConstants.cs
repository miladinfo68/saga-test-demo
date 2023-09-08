namespace Shared.Constants;

public class RabbitMqConstants
{
    public const string RabbitMqHostUri = "rabbitmq://localhost";
    public const string RabbitMqUserName = "guest";
    public const string RabbitMqPassword= "guest";

    public const string OrderTestEventQueueName = "order_test_queue";
    public const string OrderItems_Are_Exists_In_Stcok_Queue = "orderitems_are_exists_in_stcok_queue";
    
    public const string StockReservedEventQueueName = "stock_reserved_queue";
    public const string OrderStockNotReservedQueueName = "order_stock_not_reserved_queue";
    public const string StockOrderCreatedEventQueueName = "stock_order_created_queue";
    public const string OrderPaymentSucceededQueueName = "order_payment_succeeded_queue";
    public const string OrderPaymentFailedQueueName = "order_payment_failed_queue";
    public const string StockPaymentFailedQueueName = "stock_payment_failed_queue";
}