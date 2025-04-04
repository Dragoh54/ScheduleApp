namespace ScheduleService.DomainModel.Models;

public class TimeSlot : GenericEntity
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int Duration { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public TimeSlot() { }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime, int duration, string name)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
        Name = name;
    }
}