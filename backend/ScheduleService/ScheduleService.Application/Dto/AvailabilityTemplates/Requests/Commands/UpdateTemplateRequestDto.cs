using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests;

public record UpdateTemplateRequestDto
{
    public string Name { get; set; } = string.Empty;
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}