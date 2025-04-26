using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public record CreateMeetingCommand : IRequest<MeetingDto>
{
    public Guid OrganizationUserId { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    
}