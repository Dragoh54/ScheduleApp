namespace ScheduleService.DomainModel.Models;

public class DailySchedule : GenericEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public bool IsWorkingDay { get; set; }
    
    public string Name = string.Empty;
    
    public List<TimeSlot> TimeSlots { get; set; } = [];
}