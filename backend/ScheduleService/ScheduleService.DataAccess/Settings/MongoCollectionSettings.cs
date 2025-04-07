namespace ScheduleService.DataAccess.Settings;

public record MongoCollectionSettings
{
    public string AvailabilityTemplates { get; set; } = string.Empty;
    public string CalendarDays { get; set; } = string.Empty;
}