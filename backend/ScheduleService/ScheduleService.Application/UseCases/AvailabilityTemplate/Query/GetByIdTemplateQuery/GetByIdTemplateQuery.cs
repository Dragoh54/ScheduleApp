using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public class GetByIdTemplateQuery : IRequest<AvailabilityTemplateDto>
{
    public Guid Id { get; set; }
}