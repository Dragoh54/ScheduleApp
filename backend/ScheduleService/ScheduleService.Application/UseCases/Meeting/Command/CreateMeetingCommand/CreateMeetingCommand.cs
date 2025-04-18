using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingDto>
{
    public Guid UserId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}