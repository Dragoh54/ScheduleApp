using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public class GetUserTemplatesHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetUserTemplatesQuery, IEnumerable<AvailabilityTemplateDto>>
{
    public async Task<IEnumerable<AvailabilityTemplateDto>> Handle(GetUserTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await unitOfWork.AvailabilityTemplates.GetUserTemplatesAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("User templates not found");

        return templates.Adapt<IEnumerable<AvailabilityTemplateDto>>();
    }
}