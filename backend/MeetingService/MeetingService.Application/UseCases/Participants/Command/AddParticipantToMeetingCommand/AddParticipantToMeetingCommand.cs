using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public record AddParticipantToMeetingCommand : IRequest<ParticipantWithMeetingDto>
{
    public AddParticipantToMeetingCommand()
    {
    }

    public AddParticipantToMeetingCommand(Guid meetingId, AddParticipantToMeetingDto dto, string url, string accessToken)
    {
        MeetingId = meetingId;
        UserId = dto.UserId;
        Email = dto.Email;
        Username = dto.Username;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        CallbackUrl = url;
        AccessToken = accessToken;
    }

    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CallbackUrl { get; set; } = string.Empty; 
    public string AccessToken { get; set; } = string.Empty;
}