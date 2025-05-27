using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailQueryHandler : IRequestHandler<GetParticipantByEmailQuery, ParticipantWithMeetingResponseDto>
{
    public GetParticipantByEmailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<ParticipantWithMeetingResponseDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
    {
        var participant = _unitOfWork.ParticipantRepository.GetParticipantByEmail(request.MeetingId, request.Email, cancellationToken)
                          ?? throw new NotFoundException("Participant not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participant.Adapt<ParticipantWithMeetingResponseDto>();
    }
}