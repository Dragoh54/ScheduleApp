using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public class GetUserTemplatesQueryHandler : IRequestHandler<GetUserTemplatesQuery, IEnumerable<AvailabilityTemplateResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserTemplatesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<AvailabilityTemplateResponseDto>> Handle(GetUserTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _unitOfWork.AvailabilityTemplates.GetUserTemplatesAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("User templates not found");

        return templates.Adapt<IEnumerable<AvailabilityTemplateResponseDto>>();
    }
}