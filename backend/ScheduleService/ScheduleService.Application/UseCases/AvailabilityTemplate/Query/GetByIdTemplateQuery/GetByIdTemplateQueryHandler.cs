using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public class GetByIdTemplateQueryHandler : IRequestHandler<GetByIdTemplateQuery, AvailabilityTemplateDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdTemplateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateDto> Handle(GetByIdTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Template not found");

        return template.Adapt<AvailabilityTemplateDto>();
    }
}