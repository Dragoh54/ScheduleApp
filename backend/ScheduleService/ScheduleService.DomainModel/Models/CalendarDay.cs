namespace ScheduleService.DomainModel.Models;

public class CalendarDay : GenericEntity
{
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    
    //if TemplateId is null then no template to this day
    public Guid? TemplateId { get; set; }

    public List<TimeSlot>? CustomTimeSlots { get; set; }
    public bool? IsCustomDayOff { get; set; }  
    
    public CalendarDay(){}

    public CalendarDay(Guid userId, DateOnly date)
    {
        UserId = userId;
        Date = date;
    }

    public CalendarDay(Guid userId, DateOnly date, Guid? templateId)
    {
        UserId = userId;
        Date = date;
        TemplateId = templateId;
    }
}