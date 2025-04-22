namespace ScheduleService.DataAccess.Settings;

public record MongoDbSettings
{
    public string MongoConnectionString { get; init; } = string.Empty;
    public string MongoDatabaseName { get; init; } = string.Empty;
}