using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;

public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteTemplateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Template not found");

        if (template.IsDefault)
        {
            throw new BadRequestException("Default template cannot be deleted");
        }
        
        await _unitOfWork.AvailabilityTemplates.DeleteAsync(template.Id, cancellationToken);
        
        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to delete template to database");
        }
        
        return success;
    }
}