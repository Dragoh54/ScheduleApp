using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class CalendarDay : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public List<TimeSlot>? CustomTimeSlots { get; set; }
    
    public CalendarDay(){}

    public CalendarDay(Guid userId, DateOnly date)
    {
        UserId = userId;
        Date = date;
    }
}