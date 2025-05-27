using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingResponseDto>
{
    public GetMeetingByIdQuery()
    {
    }
    
    public GetMeetingByIdQuery(GetMeetingByIdRequestDto dto) => Id = dto.MeetingId;

    public Guid Id { get; set; }
}