using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;

public record UpdateTemplateRequestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}