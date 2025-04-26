using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDtos;

public record ParticipantDto
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public ParticipationStatus Status { get; set; }
    
    public MeetingDto Meeting { get; set; } = null!;
}