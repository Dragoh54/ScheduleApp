using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public record GetMeetingsWithParticipantsQuery : IRequest<IEnumerable<MeetingDto>>
{
    
}