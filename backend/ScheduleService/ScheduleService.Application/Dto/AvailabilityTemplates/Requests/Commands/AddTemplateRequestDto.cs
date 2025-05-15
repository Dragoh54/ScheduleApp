using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests;

public record AddTemplateRequestDto
{
    public string Name { get; init; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; init; } = [];
}