using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public class GetDefaultTemplateQueryHandler : IRequestHandler<GetDefaultTemplateQuery, AvailabilityTemplateResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDefaultTemplateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateResponseDto> Handle(GetDefaultTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetDefaultTemplateAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("Default template not found");

        return template.Adapt<AvailabilityTemplateResponseDto>();
    }
}