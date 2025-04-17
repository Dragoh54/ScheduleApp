namespace ScheduleService.DomainModel.Models;

public class DayOfWeekSchedule
{
    public DayOfWeek DayOfWeek { get; set; }
    public List<TimeSlot> TimeSlots { get; set; } = [];
}
