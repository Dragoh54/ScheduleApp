using RabbitMQ.Client;

namespace ScheduleService.Application.Interfaces.RabbitMQ;

public interface IRabbitMQConnection
{
    ValueTask<IConnection> GetConnectionAsync(CancellationToken cancellationToken);
}