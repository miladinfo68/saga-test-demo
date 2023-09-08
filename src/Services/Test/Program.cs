using MassTransit;
using Test.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EndpointConfig>(builder.Configuration.GetSection("Endpoints"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MyMessageConsumer>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("Queue1", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<MyMessageConsumer>(provider);
        });

    }));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class EndpointConfig
{
    public string Queue1 { get; set; }
}