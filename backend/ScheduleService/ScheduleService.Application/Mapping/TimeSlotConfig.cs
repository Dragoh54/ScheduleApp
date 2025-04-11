using Mapster;
using ScheduleService.Application.Dto;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Mapping;

public class TimeSlotConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<TimeSlot, TimeSlotDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime);

        TypeAdapterConfig<TimeSlotDto, TimeSlot>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime);
    }
}