using Core.Infrastructure;
using MassTransit;
using Microsoft.OpenApi.Models;
using Order.API.Consumers;
using Order.Infrastructure;
using Order.Application;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

//save datatime to postgres normaly
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//builder.Services.AddMediator(cfg =>
//{
//    cfg.AddConsumer<TestOrderEventConsumer>();
//});

var dependencyOptions = new DependencyOptions
{
    AddHttpClient = true,
    AddDistributedTracing = true,
    AddMessageBroker = true,
    MessageBrokerConfiguration = x =>
    {
        x.AddConsumer<TestOrderEventConsumer>();
        x.AddConsumer<StockNotReservedEventConsumer>();
        x.AddConsumer<PaymentFailedEventConsumer>();
        x.AddConsumer<PaymentSucceededEventConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), h =>
            {
                h.Username(builder.Configuration.GetSection("RabbitMQ")["UserName"]);
                h.Password(builder.Configuration.GetSection("RabbitMQ")["Password"]);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.OrderTestEventQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<TestOrderEventConsumer>(context);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.OrderStockNotReservedQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<StockNotReservedEventConsumer>(context);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.OrderPaymentSucceededQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<PaymentSucceededEventConsumer>(context);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.OrderPaymentFailedQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<PaymentFailedEventConsumer>(context);
            });

           ////////////////

        });
    }
};

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, dependencyOptions);


//builder.Services.AddApplication(typeof(CreateOrderCommandHandler));

//builder.Services.AddMassTransit();
//builder.Services.AddTransient<IMassTransitHandler, MassTransitHandler>();
//builder.Services.AddInfrastructure(builder.Configuration, x =>
//{
//    x.AddConsumer<StockNotReservedEventConsumer>();
//    x.AddConsumer<PaymentFailedEventConsumer>();
//    x.AddConsumer<PaymentSucceededEventConsumer>();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), h =>
//        {
//            h.Username(builder.Configuration.GetSection("RabbitMQ")["UserName"]);
//            h.Password(builder.Configuration.GetSection("RabbitMQ")["Password"]);
//        });

//        cfg.ReceiveEndpoint(RabbitMqConstants.OrderPaymentSucceededQueueName, e =>
//        {
//            e.ConfigureConsumer<PaymentSucceededEventConsumer>(context);
//        });

//        cfg.ReceiveEndpoint(RabbitMqConstants.OrderPaymentFailedQueueName, e =>
//        {
//            e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
//        });

//        cfg.ReceiveEndpoint(RabbitMqConstants.OrderStockNotReservedQueueName, e =>
//        {
//            e.ConfigureConsumer<StockNotReservedEventConsumer>(context);
//        });
//    });
//});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v1" }); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();