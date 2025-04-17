using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateCommand : IRequest<AvailabilityTemplateDto>
{
    public string Name { get; set; } = string.Empty;
    public  Dictionary<DayOfWeek, List<TimeSlotDto>> Schedule { get; set; } = new();
}