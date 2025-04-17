using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class AvailabilityTemplate : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public bool IsDefault { get; set; }
    
    public List<DayOfWeekSchedule> Schedule { get; set; } = [];
    
    
    public AvailabilityTemplate()
    {
        
    }

    public AvailabilityTemplate(Guid userId, string name, List<DayOfWeekSchedule> schedule)
    {
        UserId = userId;
        Name = name;
        Schedule = schedule;
    }
}