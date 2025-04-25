using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingDto>
{
    
}