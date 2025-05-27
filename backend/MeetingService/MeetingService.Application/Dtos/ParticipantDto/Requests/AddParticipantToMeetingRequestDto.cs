namespace MeetingService.Application.Dtos.ParticipantDto.Requests;

public record AddParticipantToMeetingRequestDto
{
    public Guid UserId { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}