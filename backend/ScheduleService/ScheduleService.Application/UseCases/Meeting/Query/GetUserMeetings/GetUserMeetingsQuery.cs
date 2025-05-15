using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public record GetUserMeetingsQuery : IRequest<IEnumerable<MeetingResponseDto>>
{
    public GetUserMeetingsQuery()
    {
    }
    
    public GetUserMeetingsQuery(GetUserMeetingsRequestDto dto) => UserId = dto.UserId;

    public Guid UserId { get; set; }
}