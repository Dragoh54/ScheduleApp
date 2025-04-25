using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        meeting.Status = request.Status;
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        return updatedMeeting.Adapt<MeetingDto>();
    }
}