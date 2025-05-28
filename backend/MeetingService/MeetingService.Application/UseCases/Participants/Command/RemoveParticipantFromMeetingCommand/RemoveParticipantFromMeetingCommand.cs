using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Requests;
using MeetingService.Application.Dtos.ParticipantDto.Responses;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public record RemoveParticipantFromMeetingCommand : IRequest<ParticipantWithMeetingResponseDto>
{
    public RemoveParticipantFromMeetingCommand()
    {
    }

    public RemoveParticipantFromMeetingCommand(Guid meetingId, RemoveParticipantFromMeetingRequestDto requestDto, string accessToken)
    {
        MeetingId = meetingId;
        UserId = requestDto.UserId;
        AccessToken = accessToken;
    }

    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
    public string AccessToken { get; set; }
}