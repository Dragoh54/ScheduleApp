using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public record RescheduleMeetingCommand : IRequest<MeetingDto>
{
    
}