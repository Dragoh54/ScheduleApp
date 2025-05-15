using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public record GetDefaultTemplateQuery : IRequest<AvailabilityTemplateResponseDto>
{
    public Guid UserId { get; set; }
}