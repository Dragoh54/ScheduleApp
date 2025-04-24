using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos;

public record MeetingDto
{
    public Guid Id { get; set; }
    public Guid OrganizationUserId { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public MeetingStatus Status { get; set; }
    
    public ICollection<ParticipantDto> Participants { get; set; } = [];
}