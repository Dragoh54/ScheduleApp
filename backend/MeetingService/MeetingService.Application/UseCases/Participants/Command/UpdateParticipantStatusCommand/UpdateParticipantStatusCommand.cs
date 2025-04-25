using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public record UpdateParticipantStatusCommand : IRequest<ParticipantDto>
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public ParticipationStatus Status { get; set; }
}