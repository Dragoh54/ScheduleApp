using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public record GetUserMeetingsQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid UserId { get; set; }
}