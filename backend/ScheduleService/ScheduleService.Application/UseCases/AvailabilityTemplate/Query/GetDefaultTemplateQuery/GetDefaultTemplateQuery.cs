using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public record GetDefaultTemplateQuery : IRequest<AvailabilityTemplateDto>
{
    public Guid UserId { get; set; }
}