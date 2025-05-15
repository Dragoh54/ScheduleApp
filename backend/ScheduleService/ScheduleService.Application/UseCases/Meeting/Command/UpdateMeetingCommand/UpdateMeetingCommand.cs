using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingCommand;

public record UpdateMeetingCommand : IRequest<MeetingResponseDto>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public MeetingStatus Status { get; init; }
}