using RabbitMQ.Client;

namespace MeetingService.Application.Interfaces.RabbitMQ;

public interface IRabbitMQConnection : IDisposable
{
    ValueTask<IConnection> GetConnectionAsync(CancellationToken cancellationToken);
}