using MeetingService.Application.Interfaces.RabbitMQ;
using MeetingService.Application.RabbitMQ.Options;
using RabbitMQ.Client;

namespace MeetingService.Application.RabbitMQ;

public class RabbitMQConnection : IRabbitMQConnection
{
    private IConnection? _connection;

    private readonly RabbitMQConnectionOptions _options;

    private readonly SemaphoreSlim _connectionLock;
    private Task<IConnection> _taskConnection;

    private bool disposed = false;

    public RabbitMQConnection(RabbitMQConnectionOptions options)
    {
        _options = options;
        _connectionLock = new SemaphoreSlim(1, 1);
        StartConnection();
    }

    private void StartConnection()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password,
        };

        _taskConnection = connectionFactory.CreateConnectionAsync();
    }
    
    public async ValueTask<IConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        if (_connection != null)
        {
            return _connection;
        }

        await _connectionLock.WaitAsync(cancellationToken);
        try
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = await _taskConnection;

            return _connection;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _connection?.Dispose();
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}