using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class AvailabilityTemplate : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public bool IsDefault { get; set; }
    
    public Dictionary<DayOfWeek, List<TimeSlot>> Schedule { get; set; } = new();
    
    
    public AvailabilityTemplate()
    {
        
    }

    public AvailabilityTemplate(Guid userId, string name, Dictionary<DayOfWeek, List<TimeSlot>> schedule)
    {
        UserId = userId;
        Name = name;
        Schedule = schedule;
    }
}