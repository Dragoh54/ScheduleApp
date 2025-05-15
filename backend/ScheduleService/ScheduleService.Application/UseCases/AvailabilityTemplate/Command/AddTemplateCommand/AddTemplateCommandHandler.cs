using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, AvailabilityTemplateResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddTemplateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateResponseDto> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        var addedTemplate = await _unitOfWork.AvailabilityTemplates.AddAsync(request.Adapt<DomainModel.Models.AvailabilityTemplate>(), cancellationToken)
            ?? throw new BadRequestException("Failed to add template to database");

        if (request.IsDefault)
        {
            await _unitOfWork.AvailabilityTemplates.SetDefaultTemplateAsync(addedTemplate.UserId, addedTemplate.Id, cancellationToken);
        }
            
        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to add template to database");
        }
        
        return addedTemplate.Adapt<AvailabilityTemplateResponseDto>();
    }
}