using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDtos;

public record AddParticipantToMeetingDto
{
    public Guid UserId { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}