using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Requests;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public record AddParticipantToMeetingCommand : IRequest<ParticipantWithMeetingResponseDto>
{
    public AddParticipantToMeetingCommand()
    {
    }

    public AddParticipantToMeetingCommand(Guid meetingId, AddParticipantToMeetingRequestDto requestDto, string url, string accessToken)
    {
        MeetingId = meetingId;
        UserId = requestDto.UserId;
        Email = requestDto.Email;
        Username = requestDto.Username;
        FirstName = requestDto.FirstName;
        LastName = requestDto.LastName;
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