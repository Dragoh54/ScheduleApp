namespace ScheduleService.DomainModel.Models;

public class TimeSlot
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int Duration { get; set; }
    
    public string Name { get; set; } = string.Empty;
}