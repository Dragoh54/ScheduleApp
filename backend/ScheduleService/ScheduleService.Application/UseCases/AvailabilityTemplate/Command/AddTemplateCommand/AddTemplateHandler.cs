using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddTemplateCommand, AvailabilityTemplateDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AvailabilityTemplateDto> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.AvailabilityTemplates.AddAsync(request.Adapt<DomainModel.Models.AvailabilityTemplate>(), cancellationToken);
        var success = await _unitOfWork.Commit(cancellationToken);
        
        if (!success)
        {
            throw new BadRequestException("Failed to add template to database");
        }
        
        return request.Adapt<AvailabilityTemplateDto>();
    }
}