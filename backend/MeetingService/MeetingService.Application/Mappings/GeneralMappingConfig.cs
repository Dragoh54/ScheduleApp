namespace MeetingService.Application.Mappings;

public class GeneralMappingConfig
{
    public static void RegisterMappers()
    {
        MeetingMappingConfig.RegisterMappers();
        ParticipantMappingConfig.RegisterMappers();
    }
}