using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public record AddTemplateCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public AddTemplateCommand(){}

    public AddTemplateCommand(AddTemplateRequestDto dto)
    {
        UserId = dto.UserId;
        Name = dto.Name;
        IsDefault = dto.IsDefault;
        Schedule = dto.Schedule;
    }
    
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; init; } = [];
}
