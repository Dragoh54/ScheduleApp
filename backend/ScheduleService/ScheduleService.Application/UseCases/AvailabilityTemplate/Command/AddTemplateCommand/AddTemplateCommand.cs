using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public record AddTemplateCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; init; } = [];
}
