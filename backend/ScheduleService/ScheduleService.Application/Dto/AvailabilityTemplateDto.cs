using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.Dto;

public class AvailabilityTemplateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public Dictionary<DayOfWeek, List<TimeSlotDto>> Schedule { get; set; } = new();

    public AvailabilityTemplateDto()
    {
        
    }

    public AvailabilityTemplateDto(Guid id, Guid userId, string name, bool isDefault, Dictionary<DayOfWeek, List<TimeSlot>> schedule)
    {
        Id = id;
        UserId = userId;
        
    }
}