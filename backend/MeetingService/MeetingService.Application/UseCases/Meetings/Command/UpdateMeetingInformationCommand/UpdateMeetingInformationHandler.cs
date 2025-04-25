using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public class UpdateMeetingInformationHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingInformationCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingInformationCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        request.Adapt(meeting);
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return updatedMeeting.Adapt<MeetingDto>();
    }
}