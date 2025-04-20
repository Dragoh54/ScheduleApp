using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
}