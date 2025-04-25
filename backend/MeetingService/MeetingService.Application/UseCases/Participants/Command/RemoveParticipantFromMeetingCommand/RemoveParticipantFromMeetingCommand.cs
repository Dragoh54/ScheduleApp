using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public record RemoveParticipantFromMeetingCommand : IRequest<ParticipantDto>
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
}