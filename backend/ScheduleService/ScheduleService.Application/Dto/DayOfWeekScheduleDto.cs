namespace ScheduleService.Application.Dto;

public record DayOfWeekScheduleDto
{
    public DayOfWeek DayOfWeek { get; init; }
    public List<TimeSlotDto> TimeSlots { get; init; } = [];
}