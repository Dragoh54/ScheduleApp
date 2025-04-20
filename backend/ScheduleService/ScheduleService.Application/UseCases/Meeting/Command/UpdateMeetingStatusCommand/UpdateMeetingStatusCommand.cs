using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}