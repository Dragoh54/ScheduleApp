namespace ScheduleService.DomainModel.Models;

public class DailySchedule : GenericEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public bool IsWorkingDay { get; set; }
    
    public string Name = string.Empty;
    
    public List<TimeSlot> TimeSlots { get; set; } = [];
    
    public DailySchedule(){}

    public DailySchedule(DayOfWeek dayOfWeek, bool isWorkingDay, string name, List<TimeSlot> timeSlots)
    {
        DayOfWeek = dayOfWeek;
        IsWorkingDay = isWorkingDay;
        Name = name;
        TimeSlots = timeSlots;
    }
}