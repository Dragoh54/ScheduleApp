using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public record GetUserTemplatesQuery : IRequest<IEnumerable<AvailabilityTemplateDto>>
{
    public Guid UserId { get; set; }
}