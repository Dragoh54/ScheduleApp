using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingCommand;

public class UpdateMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingCommand, MeetingDto>
{
    public Task<MeetingDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}