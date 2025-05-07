using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public record RemoveParticipantFromMeetingCommand : IRequest<ParticipantDto>
{
    public RemoveParticipantFromMeetingCommand()
    {
    }

    public RemoveParticipantFromMeetingCommand(Guid meetingId, RemoveParticipantFromMeetingDto dto, string accessToken)
    {
        MeetingId = meetingId;
        UserId = dto.UserId;
    }

    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
    public string AccessToken { get; set; }
}