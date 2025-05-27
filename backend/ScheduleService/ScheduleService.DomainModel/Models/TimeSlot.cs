using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class TimeSlot 
{
    public TimeSlot()
    {
    }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}