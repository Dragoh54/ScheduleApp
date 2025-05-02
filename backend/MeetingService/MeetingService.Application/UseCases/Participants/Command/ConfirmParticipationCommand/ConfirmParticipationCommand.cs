using MediatR;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public record ConfirmParticipationCommand : IRequest<ParticipantDto>
{
    public ConfirmParticipationCommand()
    {
    }

    public ConfirmParticipationCommand(Guid meetingId, UpdateParticipantStatusDto dto)
    {
        MeetingId = meetingId;
        Email = dto.Email;
        Token = dto.Token;
        Status = dto.Status;
    }

    public Guid MeetingId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public ParticipationStatus Status { get; set; }
}