using RabbitMQ.Client;
using ScheduleService.Application.Interfaces.RabbitMQ;
using ScheduleService.Application.RabbitMQ.Options;

namespace ScheduleService.Application.RabbitMQ.Connection;

public class RabbitMQConnection : IRabbitMQConnection
{
    public RabbitMQConnection(RabbitMQConnectionOptions options)
    {
        _options = options;
        _connectionLock = new SemaphoreSlim(1, 1);
        
        InitializeConnection();
    }
    
    private IConnection? _connection;

    private readonly RabbitMQConnectionOptions _options;

    private readonly SemaphoreSlim _connectionLock;
    private Task<IConnection> _taskConnection;

    private bool disposed = false;

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
    
    private void InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password,
        };

        factory.RequestedHeartbeat = TimeSpan.FromSeconds(30);

        _taskConnection = factory.CreateConnectionAsync();
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