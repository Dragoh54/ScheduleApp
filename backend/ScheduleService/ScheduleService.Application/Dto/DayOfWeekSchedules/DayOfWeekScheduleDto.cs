using ScheduleService.Application.Dto.TimeSlots;

namespace ScheduleService.Application.Dto.DayOfWeekSchedules;

public record DayOfWeekScheduleDto
{
    public DayOfWeek DayOfWeek { get; init; }
    public List<TimeSlotDto> TimeSlots { get; init; } = [];
}