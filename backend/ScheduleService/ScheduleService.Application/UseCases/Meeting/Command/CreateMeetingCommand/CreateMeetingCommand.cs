using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Requests.Commands;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingResponseDto>
{
    public CreateMeetingCommand()
    {
    }

    public CreateMeetingCommand(CreateMeetingRequestDto dto)
    {
        UserId = dto.UserId;
        StartTime = dto.StartTime;
        EndTime = dto.EndTime;
        Status = dto.Status;
    }

    public Guid UserId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}