namespace ScheduleService.DomainModel.Models;

//base calendar day
//if TemplateId is null then no template to this day
public class CalendarDay
{
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    
    public Guid? TemplateId { get; set; }
    
    public List<TimeSlot>? CustomTimeSlots { get; set; }
    public bool? IsCustomDayOff { get; set; }  
}