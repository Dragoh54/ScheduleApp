using MediatR;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public record ConfirmParticipationCommand : IRequest<ParticipantWithMeetingDto>
{
    public ConfirmParticipationCommand()
    {
    }

    public ConfirmParticipationCommand(Guid meetingId, ConfirmParticipantStatusDto dto)
    {
        MeetingId = meetingId;
        Email = dto.Email;
        Token = dto.Token;
        ParticipationStatusString = dto.ParticipationStatusString;
    }

    public Guid MeetingId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string ParticipationStatusString { get; set; }
}