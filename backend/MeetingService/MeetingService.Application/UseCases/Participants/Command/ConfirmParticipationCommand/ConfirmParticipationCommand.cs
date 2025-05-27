using MediatR;
using MeetingService.Application.Dtos.ParticipantDto.Requests;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public record ConfirmParticipationCommand : IRequest<ParticipantWithMeetingResponseDto>
{
    public ConfirmParticipationCommand()
    {
    }

    public ConfirmParticipationCommand(Guid meetingId, ConfirmParticipantStatusRequestDto requestDto)
    {
        MeetingId = meetingId;
        Email = requestDto.Email;
        Token = requestDto.Token;
        ParticipationStatusString = requestDto.ParticipationStatusString;
    }

    public Guid MeetingId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string ParticipationStatusString { get; set; }
}