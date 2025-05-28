using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScheduleService.Application.Interfaces.RabbitMQ;
using ScheduleService.Application.Interfaces.RabbitMQ.Consumers;

namespace ScheduleService.Application.RabbitMQ.Consumers;

public class MessageConsumer : IMessageConsumer
{
    public MessageConsumer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }
    
    private readonly IRabbitMQConnection _connection;

    private IChannel _channel;
    
    public async Task ConsumeAsync(string queueName, AsyncEventHandler<BasicDeliverEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        var establishedConnection = await _connection.GetConnectionAsync(cancellationToken);

        _channel = await establishedConnection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(queueName, exclusive: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += eventHandler;

        await _channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is null)
        {
            return;
        }

        await _channel.CloseAsync(cancellationToken);
    }
}