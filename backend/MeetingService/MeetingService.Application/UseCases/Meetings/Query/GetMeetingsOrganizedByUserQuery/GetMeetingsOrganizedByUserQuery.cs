using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public record GetMeetingsOrganizedByUserQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid OrganizerId { get; set; }
}