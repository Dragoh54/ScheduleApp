using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public record GetMeetingsOrganizedByUserQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid OrganizerId { get; set; }
}