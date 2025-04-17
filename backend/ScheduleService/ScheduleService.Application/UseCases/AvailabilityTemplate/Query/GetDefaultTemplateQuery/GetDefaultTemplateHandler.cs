using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public class GetDefaultTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetDefaultTemplateQuery, AvailabilityTemplateDto>
{
    public async Task<AvailabilityTemplateDto> Handle(GetDefaultTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await unitOfWork.AvailabilityTemplates.GetDefaultTemplateAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("Default template not found");

        return template.Adapt<AvailabilityTemplateDto>();
    }
}