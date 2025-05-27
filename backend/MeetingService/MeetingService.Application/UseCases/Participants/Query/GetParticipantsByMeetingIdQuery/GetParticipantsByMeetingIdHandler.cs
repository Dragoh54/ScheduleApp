using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public class GetParticipantsByMeetingIdHandler : IRequestHandler<GetParticipantsByMeetingIdQuery, IEnumerable<ParticipantWithMeetingDto>>
{
    GetParticipantsByMeetingIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<IEnumerable<ParticipantWithMeetingDto>> Handle(GetParticipantsByMeetingIdQuery request, CancellationToken cancellationToken)
    {
        var participants = await _unitOfWork.ParticipantRepository.GetParticipantsByMeetingId(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return participants.Adapt<IEnumerable<ParticipantWithMeetingDto>>();
    }
}