using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public class GetMeetingByIdHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingByIdQuery, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meeting.Adapt<MeetingWithParticipantsDto>();
    }
}