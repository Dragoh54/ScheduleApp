namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;

public record GetUserTemplatesRequestDto
{
    public Guid UserId { get; set; }
}