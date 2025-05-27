using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.MeetingDto.Requests;

public record UpdateMeetingStatusRequestDto
{
    public MeetingStatus Status { get; set; }
}