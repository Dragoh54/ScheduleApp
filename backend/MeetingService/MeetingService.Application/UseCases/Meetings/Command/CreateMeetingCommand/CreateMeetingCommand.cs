using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingDto>
{
    public CreateMeetingCommand(CreateMeetingDto dto, string accessToken)
    {
        AccessToken = accessToken;
        Title = dto.Title;
        Description = dto.Description;
        StartTime = dto.StartTime;
        EndTime = dto.EndTime;
    }
    
    public string AccessToken { get; set; } = string.Empty;
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}