using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddTemplateCommand, AvailabilityTemplateDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<AvailabilityTemplateDto> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}