using System.Text;
using System.Text.Json;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using ScheduleService.Application.Interfaces.RabbitMQ.Consumers;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.Application.RabbitMQ.Dto;
using ScheduleService.Application.RabbitMQ.Options;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.RabbitMQ.Consumers;

public class SubscriptionConsumer : BackgroundService
{
    public SubscriptionConsumer(IMessageConsumer messageConsumer,
        IOptions<RabbitMQConnectionOptions> options,
        IServiceScopeFactory serviceScopeFactory)
    {
        _messageConsumer = messageConsumer;
        _options = options.Value;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    private readonly IMessageConsumer _messageConsumer;
    private readonly RabbitMQConnectionOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var isSubscriptionNameEmpty = string.IsNullOrWhiteSpace(_options.SubscriptionQueueName);

        if (isSubscriptionNameEmpty)
        {
            return;
        }
        
        Console.WriteLine($"Subscribing to queue: {_options.SubscriptionQueueName}");

        await _messageConsumer.ConsumeAsync(_options.SubscriptionQueueName, SubscriptionRecievedAsync, stoppingToken);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _messageConsumer.StopAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
    
    private async Task SubscriptionRecievedAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        
        MeetingFromRabbitDto? meetingDto;
        var isValidMessage = TryExtractMessage(message, out meetingDto);

        if (!isValidMessage)
        {
            return;
        }

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.Meetings.AddAsync(meetingDto.Adapt<Meeting>(), CancellationToken.None);
        }
    }

    private bool TryExtractMessage(string message, out MeetingFromRabbitDto? messageDto)
    {
        messageDto = null;

        if (string.IsNullOrWhiteSpace(message))
            return false;

        try
        {
            messageDto = JsonSerializer.Deserialize<MeetingFromRabbitDto>(message);
            return messageDto is not null;
        }
        catch
        {
            return false;
        }
    }
}