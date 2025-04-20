using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public record GetByIdTemplateQuery : IRequest<AvailabilityTemplateDto>
{
    public Guid Id { get; set; }
}