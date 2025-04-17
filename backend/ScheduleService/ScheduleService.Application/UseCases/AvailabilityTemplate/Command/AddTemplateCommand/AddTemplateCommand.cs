using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public record AddTemplateCommand : IRequest<AvailabilityTemplateDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; init; } = [];
}
