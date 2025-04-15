namespace ScheduleService.Application.Dto;

public record DayOfWeekSchedule
{
    public DayOfWeek DayOfWeek { get; init; }
    public List<TimeSlotDto> TimeSlots { get; init; } = [];
}