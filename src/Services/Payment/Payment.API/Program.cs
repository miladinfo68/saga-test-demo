using Core.Infrastructure;
using MassTransit;
using Microsoft.OpenApi.Models;
using Payment.API.Consumers;
using Payment.Application;
using Payment.Infrastructure;
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
        x.AddConsumer<StockReservedEventConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), h =>
            {
                h.Username(builder.Configuration.GetSection("RabbitMQ")["UserName"]);
                h.Password(builder.Configuration.GetSection("RabbitMQ")["Password"]);
            });

            cfg.ReceiveEndpoint(RabbitMqConstants.StockReservedEventQueueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<StockReservedEventConsumer>(context);
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment.API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
