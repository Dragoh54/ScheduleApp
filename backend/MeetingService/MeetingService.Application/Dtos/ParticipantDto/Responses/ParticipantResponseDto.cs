using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDto.Responses;

public record ParticipantResponseDto
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public ParticipationStatus Status { get; set; }
}