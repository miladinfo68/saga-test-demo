using Shared.Constants;
using Microsoft.OpenApi.Models;
using Stock.API.Consumers;
using Stock.Infrastructure;
using Stock.Application;
using Core.Infrastructure;
using MassTransit;

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
        x.AddConsumer<OrderCreatedEventConsumer>();
        x.AddConsumer<PaymentFailedEventConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), h =>
            {
                h.Username(builder.Configuration.GetSection("RabbitMQ")["UserName"]);
                h.Password(builder.Configuration.GetSection("RabbitMQ")["Password"]);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.StockOrderCreatedEventQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<OrderCreatedEventConsumer>(context);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.StockPaymentFailedQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<PaymentFailedEventConsumer>(context);
            });

        });
    }
};

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, dependencyOptions);





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock.API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
