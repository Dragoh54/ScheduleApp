using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DomainModel.Models;

public class Meeting : IEntity
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}