namespace MeetingService.Application.Interfaces.RabbitMQ.Services;

public interface IRabbitMQService
{
    Task SendSubscriptionMessage<T>(T message, CancellationToken cancellationToken);
}