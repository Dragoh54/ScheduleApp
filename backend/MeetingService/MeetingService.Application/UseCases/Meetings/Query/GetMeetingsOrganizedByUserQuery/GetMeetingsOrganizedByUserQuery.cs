using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public record GetMeetingsOrganizedByUserQuery : IRequest<IEnumerable<MeetingWithParticipantsDto>>
{
    public Guid OrganizerId { get; set; }
}