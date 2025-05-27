using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Interfaces;

namespace MeetingService.DomainModel.Models;

public class Meeting : IdEntity
{
    public Guid OrganizationUserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime NotifyTime { get; set; }
    
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;   
    
    public ICollection<Participant> Participants { get; set; } = [];
}