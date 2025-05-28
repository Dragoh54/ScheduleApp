using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.Dtos.MeetingDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingResponseDto>
{
    public CreateMeetingCommand(CreateMeetingRequestDto requestDto, string accessToken)
    {
        AccessToken = accessToken;
        Title = requestDto.Title;
        Description = requestDto.Description;
        StartTime = requestDto.StartTime;
        EndTime = requestDto.EndTime;
    }
    
    public string AccessToken { get; set; } = string.Empty;
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime? NotifyTime { get; set; }
}