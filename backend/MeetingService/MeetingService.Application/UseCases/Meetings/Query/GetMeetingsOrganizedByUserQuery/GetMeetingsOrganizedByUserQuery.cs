using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public record GetMeetingsOrganizedByUserQuery : IRequest<IEnumerable<MeetingWithParticipantsResponseDto>>
{
    public Guid OrganizerId { get; set; }
}