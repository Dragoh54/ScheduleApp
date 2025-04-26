using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public record UpdateParticipantStatusCommand : IRequest<ParticipantDto>
{
    public UpdateParticipantStatusCommand()
    {
    }

    public UpdateParticipantStatusCommand(Guid meetingId, UpdateParticipantStatusDto dto)
    {
        MeetingId = meetingId;
        UserId = dto.UserId;
        Status = dto.Status;
    }

    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
    public ParticipationStatus Status { get; set; }
}