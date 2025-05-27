namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;

public record IsUserFreeRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}