using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public record UpdateTemplateCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}