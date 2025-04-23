using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Interfaces;

namespace MeetingService.DomainModel.Models;

public class Meeting : IdEntity
{
    public Guid UserId { get; set; }  
    public TimeSlot TimeSlot { get; set; } = null!;
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;   
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}