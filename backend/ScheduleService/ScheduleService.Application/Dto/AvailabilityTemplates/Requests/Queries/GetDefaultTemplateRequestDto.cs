namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;

public record GetDefaultTemplateRequestDto
{
    public Guid UserId { get; set; }
}