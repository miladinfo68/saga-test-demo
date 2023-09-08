using Shared.Base;

namespace Core.Application.BusActions;

public interface IMassTransitHandler
{
    Task Publish(IDomainEvent @event, Type type);

    Task Send(string queueName, IDomainEvent @event, Type type);
}