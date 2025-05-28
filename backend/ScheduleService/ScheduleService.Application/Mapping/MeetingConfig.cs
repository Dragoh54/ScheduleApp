using Mapster;
using ScheduleService.Application.RabbitMQ.Dto;
using ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Mapping;

public class MeetingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CreateMeetingCommand, Meeting>.NewConfig()
            .Map(dest => dest.Id, _ => Guid.NewGuid())
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, src => src.Status);
        
        TypeAdapterConfig<MeetingFromRabbitDto, Meeting>.NewConfig()
            .Map(dest => dest.Id, _ => Guid.NewGuid())
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, src => src.Status);
    }
}