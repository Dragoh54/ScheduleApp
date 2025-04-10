using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public class GetDefaultTemplateQuery : IRequest<AvailabilityTemplateDto>
{
    public Guid UserId { get; set; }
}