using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public record GetMeetingWithParticipantsQuery : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
}