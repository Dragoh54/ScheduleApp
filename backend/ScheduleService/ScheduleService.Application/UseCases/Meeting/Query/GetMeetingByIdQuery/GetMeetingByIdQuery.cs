using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingResponseDto>
{
    public Guid Id { get; set; }
}