namespace ScheduleService.Application.Mapping;

public class GeneralConfig
{
    public static void RegisterMappers()
    {
        TimeSlotConfig.RegisterMappings();
        AvailabilityTemplateConfig.RegisterMappings();
        CalendarDayConfig.RegisterMappings();
    }
}