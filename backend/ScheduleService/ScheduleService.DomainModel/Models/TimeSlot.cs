using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class TimeSlot 
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    
    public TimeSlot() { }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public static bool IsOverlapping(TimeSlot a, TimeSlot b)
    {
        return a.StartTime < b.EndTime && b.StartTime < a.EndTime;
    }
}