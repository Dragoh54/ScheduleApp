using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public record AddTemplateCommand : IRequest<AvailabilityTemplateDto>
{
    public Guid UserId { get; init; }
    public string Name { get; set; } = string.Empty;
    public  Dictionary<DayOfWeek, List<TimeSlotDto>> Schedule { get; set; } = new();
}