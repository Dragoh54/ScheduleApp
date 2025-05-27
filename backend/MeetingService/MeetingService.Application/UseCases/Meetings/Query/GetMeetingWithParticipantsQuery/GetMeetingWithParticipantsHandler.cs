using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingWithParticipantsHandler : IRequestHandler<GetMeetingWithParticipantsQuery, MeetingWithParticipantsDto>
{
    public GetMeetingWithParticipantsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<MeetingWithParticipantsDto> Handle(GetMeetingWithParticipantsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return meetings.Adapt<MeetingWithParticipantsDto>();
    }
}