using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public record GetByIdTemplateQuery : IRequest<AvailabilityTemplateResponseDto>
{
    public GetByIdTemplateQuery()
    {
    }
    
    public GetByIdTemplateQuery(GetTemplateByIdRequestDto dto) => Id = dto.Id;

    public Guid Id { get; set; }
}