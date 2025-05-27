namespace ScheduleService.Application.Dto.TimeSlots;

public record TimeSlotDto
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}