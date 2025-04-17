using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Dto;

public record AvailabilityTemplateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}