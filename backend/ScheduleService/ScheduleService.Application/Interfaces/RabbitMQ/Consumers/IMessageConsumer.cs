using RabbitMQ.Client.Events;

namespace ScheduleService.Application.Interfaces.RabbitMQ.Consumers;

public interface IMessageConsumer
{
    Task ConsumeAsync(string queueName, AsyncEventHandler<BasicDeliverEventArgs> eventHandler, CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}