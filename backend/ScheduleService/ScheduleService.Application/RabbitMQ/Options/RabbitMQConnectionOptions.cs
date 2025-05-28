namespace ScheduleService.Application.RabbitMQ.Options;

public class RabbitMQConnectionOptions
{
    public string Hostname { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SubscriptionQueueName { get; set; } = string.Empty;
}