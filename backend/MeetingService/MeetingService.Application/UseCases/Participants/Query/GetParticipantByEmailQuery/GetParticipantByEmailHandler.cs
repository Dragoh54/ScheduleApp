using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailHandler : IRequestHandler<GetParticipantByEmailQuery, ParticipantWithMeetingDto>
{
    GetParticipantByEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<ParticipantWithMeetingDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
    {
        var participant = _unitOfWork.ParticipantRepository.GetParticipantByEmail(request.MeetingId, request.Email, cancellationToken)
                          ?? throw new NotFoundException("Participant not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participant.Adapt<ParticipantWithMeetingDto>();
    }
}