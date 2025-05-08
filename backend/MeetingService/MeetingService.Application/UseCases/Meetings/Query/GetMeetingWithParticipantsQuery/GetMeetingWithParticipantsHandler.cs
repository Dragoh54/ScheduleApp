using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingWithParticipantsHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingWithParticipantsQuery, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(GetMeetingWithParticipantsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return meetings.Adapt<MeetingWithParticipantsDto>();
    }
}