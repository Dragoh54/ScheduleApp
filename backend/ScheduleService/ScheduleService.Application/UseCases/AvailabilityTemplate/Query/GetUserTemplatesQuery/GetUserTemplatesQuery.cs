using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public class GetUserTemplatesQuery : IRequest<IEnumerable<AvailabilityTemplateDto>>
{
    
}