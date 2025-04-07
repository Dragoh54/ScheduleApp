using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class TimeSlot : IEntity
{
    public Guid Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    
    public TimeSlot() { }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}