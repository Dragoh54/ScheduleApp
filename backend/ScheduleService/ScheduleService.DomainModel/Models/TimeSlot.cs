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
}