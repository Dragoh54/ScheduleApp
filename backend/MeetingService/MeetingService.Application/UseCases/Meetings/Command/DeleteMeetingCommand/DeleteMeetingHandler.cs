using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var success = await unitOfWork.MeetingRepository.Delete(meeting, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        return meeting.Adapt<MeetingDto>();
    }
}