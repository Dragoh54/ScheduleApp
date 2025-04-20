using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public record UpdateTemplateCommand : IRequest<AvailabilityTemplateDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}