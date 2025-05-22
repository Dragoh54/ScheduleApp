using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Commands;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingResponseDto>
{
    public UpdateMeetingStatusCommand()
    {
    }

    public UpdateMeetingStatusCommand(Guid meetingId, UpdateMeetingStatusRequestDto dto)
    {
        Id = meetingId;
        Status = dto.Status;
    }

    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}