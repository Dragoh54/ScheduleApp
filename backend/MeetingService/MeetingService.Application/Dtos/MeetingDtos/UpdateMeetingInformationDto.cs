namespace MeetingService.Application.Dtos.MeetingDtos;

public record UpdateMeetingInformationDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}