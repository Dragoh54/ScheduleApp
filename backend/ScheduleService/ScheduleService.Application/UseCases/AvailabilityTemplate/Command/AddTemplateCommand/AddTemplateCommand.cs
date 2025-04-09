using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateCommand : IRequest<AvailabilityTemplateDto>
{
    public string Name { get; set; } = string.Empty;
    public  Dictionary<DayOfWeek, List<TimeSlotDto>> Schedule { get; set; } = new();
}