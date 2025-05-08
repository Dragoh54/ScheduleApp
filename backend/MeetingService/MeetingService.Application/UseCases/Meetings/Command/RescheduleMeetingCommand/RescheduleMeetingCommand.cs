using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public record RescheduleMeetingCommand : IRequest<MeetingWithParticipantsDto>
{
    public RescheduleMeetingCommand()
    {
    }
    
    public RescheduleMeetingCommand(Guid meetingId, RescheduleMeetingDto dto)
    {
        Id = meetingId;
        StartTime = dto.StartTime;
        EndTime = dto.EndTime;
    }
    
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}