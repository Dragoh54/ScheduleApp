using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public record RescheduleMeetingCommand : IRequest<MeetingWithParticipantsResponseDto>
{
    public RescheduleMeetingCommand()
    {
    }
    
    public RescheduleMeetingCommand(Guid meetingId, RescheduleMeetingRequestDto requestDto)
    {
        Id = meetingId;
        StartTime = requestDto.StartTime;
        EndTime = requestDto.EndTime;
        NotifyTime = requestDto.NotifyTime;    
    }
    
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime? NotifyTime { get; set; }
}