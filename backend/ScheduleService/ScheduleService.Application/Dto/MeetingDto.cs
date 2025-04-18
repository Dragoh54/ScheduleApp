using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto;

public record MeetingDto
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}