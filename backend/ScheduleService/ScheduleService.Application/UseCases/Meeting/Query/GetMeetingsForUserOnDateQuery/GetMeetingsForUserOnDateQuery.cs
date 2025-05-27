using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserOnDateQuery;

public record GetMeetingsForUserOnDateQuery : IRequest<IEnumerable<MeetingResponseDto>>
{
    public GetMeetingsForUserOnDateQuery()
    {
    }

    public GetMeetingsForUserOnDateQuery(Guid userId, GetMeetingsOnDateRequestDto dto)
    {
        UserId = userId;
        Date = dto.Date;
    }

    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}