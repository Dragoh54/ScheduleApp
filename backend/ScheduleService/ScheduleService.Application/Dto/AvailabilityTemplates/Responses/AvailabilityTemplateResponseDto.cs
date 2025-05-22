using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

public record AvailabilityTemplateResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}