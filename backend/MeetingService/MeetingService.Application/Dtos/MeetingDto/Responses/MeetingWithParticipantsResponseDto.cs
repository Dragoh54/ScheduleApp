using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.MeetingDto.Responses;

public record MeetingWithParticipantsResponseDto
{
    public Guid Id { get; set; }
    public Guid OrganizationUserId { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime NotifyTime { get; set; }
    
    public MeetingStatus Status { get; set; }
    
    public ICollection<ParticipantResponseDto> Participants { get; set; } = [];
}