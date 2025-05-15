using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public record GetByIdTemplateQuery : IRequest<AvailabilityTemplateResponseDto>
{
    public Guid Id { get; set; }
}