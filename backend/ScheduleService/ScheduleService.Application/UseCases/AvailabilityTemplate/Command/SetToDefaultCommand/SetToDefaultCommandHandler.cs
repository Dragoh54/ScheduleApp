using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public class SetToDefaultCommandHandler : IRequestHandler<SetToDefaultCommand, AvailabilityTemplateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public SetToDefaultCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateDto> Handle(SetToDefaultCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.AvailabilityTemplates.SetDefaultTemplateAsync(request.UserId, request.TemplateId, cancellationToken);
        
        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to set to default availability template");
        }
        
        var updatedTemplate = await _unitOfWork.AvailabilityTemplates.GetByIdAsync(request.TemplateId, cancellationToken);
        
        return updatedTemplate.Adapt<AvailabilityTemplateDto>();
    }
}