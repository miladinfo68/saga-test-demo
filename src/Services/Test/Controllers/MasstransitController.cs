using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Test.Controllers;

[ApiController]
[Route("[controller]")]
public class MasstransitController : ControllerBase
{
    private readonly IBus _bus;

    private readonly IOptions<EndpointConfig> _endpointConfig;
    public MasstransitController(IBus bus, IOptions<EndpointConfig> endpointConfig)
    {
        _bus = bus;
        _endpointConfig = endpointConfig;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string text)
    {
        var message = new MyMessage { Text = text };

        var queueToPublishTo = new Uri(_endpointConfig.Value.Queue1);
        var endPoint = await _bus.GetSendEndpoint(queueToPublishTo);
        await endPoint.Send(message);

        return Ok();
    }
}

public class MyMessage
{
    public string Text { get; set; }
}

public class MyMessageConsumer : IConsumer<MyMessage>
{

    public async Task Consume(ConsumeContext<MyMessage> context)
    {
        //File.AppendAllText("mbark.txt", $"Recieved data:{context.Message.Text}");
        Console.WriteLine($"xxx\t{context.Message.Text}");
        await Task.CompletedTask;
    }
}


//https://blog.devops.dev/mastering-distributed-messaging-with-net-6-and-masstransit-building-a-robust-product-api-b914befc65d1