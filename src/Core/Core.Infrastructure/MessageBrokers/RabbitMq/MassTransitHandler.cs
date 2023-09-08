using Core.Application.BusActions;
using MassTransit;
using Shared.Base;

namespace Core.Infrastructure.MessageBrokers.RabbitMq;

public class MassTransitHandler : IMassTransitHandler
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public MassTransitHandler(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Publish(IDomainEvent @event, Type type)
    {
        await _publishEndpoint.Publish(@event, type);
    }

    public async Task Send(string queueName, IDomainEvent @event, Type type)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new System.Uri($"queue:{queueName}"));

        await sendEndpoint.Send(@event, type);
    }
}