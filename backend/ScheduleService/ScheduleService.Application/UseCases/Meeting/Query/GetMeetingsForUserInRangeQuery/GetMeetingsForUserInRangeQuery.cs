using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserInRangeQuery;

public record GetMeetingsForUserInRangeQuery : IRequest<IEnumerable<MeetingResponseDto>>
{
    public GetMeetingsForUserInRangeQuery()
    {
    }

    public GetMeetingsForUserInRangeQuery(Guid userId, GetMeetingsInRangeRequestDto dto)
    {
        UserId = userId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }

    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}