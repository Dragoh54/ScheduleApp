using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsInRangeQuery;

public record GetMeetingsInRangeQuery : IRequest<IEnumerable<MeetingDto>>
{
    
}