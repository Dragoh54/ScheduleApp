using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsForUserQuery;

public record GetMeetingsForUserQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid OrganizerId { get; set; }
}