using System.Text;
using MeetingService.Application.Interfaces.RabbitMQ;
using MeetingService.Application.Interfaces.RabbitMQ.Producers;
using RabbitMQ.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MeetingService.Application.RabbitMQ.Producers;

public class MessageProducer : IMessageProducer
{
    public MessageProducer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }
    
    private readonly IRabbitMQConnection _connection;
    
    public async Task SendMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken)
    {
        var establishedConnection = await _connection.GetConnectionAsync(cancellationToken);

        using (var channel = await establishedConnection.CreateChannelAsync(cancellationToken: cancellationToken))
        {
            await channel.QueueDeclareAsync(queueName, exclusive: false, cancellationToken: cancellationToken);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body, cancellationToken: cancellationToken);
        }
    }
}