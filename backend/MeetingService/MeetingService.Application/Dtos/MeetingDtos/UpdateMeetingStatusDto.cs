using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.MeetingDtos;

public record UpdateMeetingStatusDto
{
    public MeetingStatus Status { get; set; }
}