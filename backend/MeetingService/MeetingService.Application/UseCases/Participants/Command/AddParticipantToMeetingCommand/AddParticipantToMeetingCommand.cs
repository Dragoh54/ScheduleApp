using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public record AddParticipantToMeetingCommand : IRequest<ParticipantDto>
{
    public AddParticipantToMeetingCommand()
    {
    }

    public AddParticipantToMeetingCommand(Guid meetingId, AddParticipantToMeetingDto dto)
    {
        MeetingId = meetingId;
        UserId = dto.UserId;
        Email = dto.Email;
        Username = dto.Username;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Status = dto.Status;
    }

    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ParticipationStatus Status { get; set; } = ParticipationStatus.Pending;
}