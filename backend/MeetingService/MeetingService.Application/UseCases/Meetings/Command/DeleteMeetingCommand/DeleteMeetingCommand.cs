using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public record DeleteMeetingCommand : IRequest<MeetingWithParticipantsDto>
{
    public DeleteMeetingCommand(DeleteMeetingDto dto, string token)
    {
        Token = token;
        Id = dto.Id;
    }
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty; 
}