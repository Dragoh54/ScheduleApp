using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public record DeleteMeetingCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}