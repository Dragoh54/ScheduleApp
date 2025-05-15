namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;

public record GetTemplateByIdRequestDto
{
    public Guid Id { get; init; }
}