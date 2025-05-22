using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Commands;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public record DeleteMeetingCommand : IRequest<bool>
{
    public DeleteMeetingCommand()
    {
    }

    public DeleteMeetingCommand(DeleteMeetingRequestDto dto) => Id = dto.MeetingId;

    public Guid Id { get; set; }
}