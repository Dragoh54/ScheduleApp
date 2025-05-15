using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public record GetUserMeetingsQuery : IRequest<IEnumerable<MeetingResponseDto>>
{
    public Guid UserId { get; set; }
}