using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Commands;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingCommand;

public record UpdateMeetingCommand : IRequest<MeetingResponseDto>
{
    public UpdateMeetingCommand()
    {
    }

    public UpdateMeetingCommand(Guid meetingId, UpdateMeetingRequestDto dto)
    {
        Id = meetingId;
        UserId = dto.UserId;
        StartTime = dto.StartTime;
        EndTime = dto.EndTime;
        Status = dto.Status;
    }

    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public MeetingStatus Status { get; init; }
}