using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.RabbitMQ.Dto;

public record MeetingFromRabbitDto
{
    public Guid UserId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}