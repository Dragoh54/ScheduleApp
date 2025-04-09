namespace ScheduleService.Application.Dto;

public class TimeSlotDto
{
    public Guid Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}