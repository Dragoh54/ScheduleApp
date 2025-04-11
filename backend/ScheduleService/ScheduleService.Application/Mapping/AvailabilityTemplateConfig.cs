using Mapster;
using ScheduleService.Application.Dto;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Mapping;

public class AvailabilityTemplateConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<AddTemplateCommand, AvailabilityTemplate>
            .NewConfig()
            .Map(dest => dest.UserId, _ => Guid.Empty)
            .Map(dest => dest.IsDefault, _ => false)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Schedule, src => src.Schedule.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Adapt<List<TimeSlot>>()));
        
        TypeAdapterConfig<AvailabilityTemplate, AvailabilityTemplateDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.IsDefault, src => src.IsDefault)
            .Map(dest => dest.Schedule, src => src.Schedule.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Adapt<List<TimeSlotDto>>()));
        
        TypeAdapterConfig<UpdateTemplateCommand, AvailabilityTemplate>
            .NewConfig()
            .Ignore(dest => dest.Id) 
            .Ignore(dest => dest.UserId) 
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Schedule, src => src.Schedule.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Adapt<List<TimeSlot>>()));
    }
}