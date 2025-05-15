using Mapster;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Dto.DayOfWeekSchedules;
using ScheduleService.Application.Dto.TimeSlots;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Mapping;

public class AvailabilityTemplateConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig.GlobalSettings.ForType<AddTemplateCommand, AvailabilityTemplate>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Name, src => src.Name.Trim())
            .Map(dest => dest.IsDefault, src => src.IsDefault)
            .Map(dest => dest.Schedule, src => src.Schedule.Select(s => new DayOfWeekSchedule
            {
                DayOfWeek = s.DayOfWeek,
                TimeSlots = s.TimeSlots.Select(t => new TimeSlot(t.StartTime, t.EndTime)).ToList()
            }).ToList())
            .IgnoreNonMapped(true);

        TypeAdapterConfig.GlobalSettings.ForType<AvailabilityTemplate, AvailabilityTemplateResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.IsDefault, src => src.IsDefault)
            .Map(dest => dest.Schedule, src => src.Schedule.Select(s => new DayOfWeekScheduleDto
            {
                DayOfWeek = s.DayOfWeek,
                TimeSlots = s.TimeSlots.Select(t => new TimeSlotDto
                {
                    StartTime = t.StartTime,
                    EndTime = t.EndTime
                }).ToList()
            }).ToList())
            .IgnoreNonMapped(true);

        TypeAdapterConfig<UpdateTemplateCommand, AvailabilityTemplate>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.UserId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Schedule, src => src.Schedule.Select(s => new DayOfWeekSchedule
            {
                DayOfWeek = s.DayOfWeek,  
                TimeSlots = s.TimeSlots.Select(t => new TimeSlot(t.StartTime, t.EndTime)).ToList()
            }).ToList());

    }
}