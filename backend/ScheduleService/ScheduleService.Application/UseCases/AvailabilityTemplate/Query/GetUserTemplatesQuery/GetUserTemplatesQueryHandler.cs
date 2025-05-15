using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public class GetUserTemplatesQueryHandler : IRequestHandler<GetUserTemplatesQuery, IEnumerable<AvailabilityTemplateDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserTemplatesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<AvailabilityTemplateDto>> Handle(GetUserTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _unitOfWork.AvailabilityTemplates.GetUserTemplatesAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("User templates not found");

        return templates.Adapt<IEnumerable<AvailabilityTemplateDto>>();
    }
}