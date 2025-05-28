using MeetingService.Application.Interfaces.RabbitMQ.Producers;
using MeetingService.Application.Interfaces.RabbitMQ.Services;
using MeetingService.Application.RabbitMQ.Options;
using Microsoft.Extensions.Options;

namespace MeetingService.Application.RabbitMQ.Services;

public class RabbitMQService : IRabbitMQService
{
    public RabbitMQService(IOptions<RabbitMQConnectionOptions> options, IMessageProducer messageProducer)
    {
        _options = options.Value;
        _messageProducer = messageProducer;
    }
    
    private readonly RabbitMQConnectionOptions _options;
    private readonly IMessageProducer _messageProducer;
    
    public async Task SendSubscriptionMessage<T>(T message, CancellationToken cancellationToken)
    {
        var isSubscriptionNameEmpty = string.IsNullOrWhiteSpace(_options.SubscriptionQueueName);

        if (isSubscriptionNameEmpty)
        {
            return;
        }

        await _messageProducer.SendMessageAsync(message, _options.SubscriptionQueueName, cancellationToken);
    }
}