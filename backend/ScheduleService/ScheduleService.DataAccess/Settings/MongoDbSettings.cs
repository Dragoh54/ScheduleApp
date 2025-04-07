namespace ScheduleService.DataAccess.Settings;

public class MongoDbSettings
{
    public string MongoConnectionString { get; set; } = string.Empty;
    public string MongoDatabaseName { get; set; } = string.Empty;
}