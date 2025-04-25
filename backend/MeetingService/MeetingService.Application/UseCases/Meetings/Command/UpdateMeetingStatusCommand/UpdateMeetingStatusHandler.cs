using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto>
{
    public Task<MeetingDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}