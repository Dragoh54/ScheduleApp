using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Responses;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public class GetMeetingsOrganizedByUserQueryHandler : IRequestHandler<GetMeetingsOrganizedByUserQuery, IEnumerable<MeetingWithParticipantsResponseDto>>
{
    public GetMeetingsOrganizedByUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<IEnumerable<MeetingWithParticipantsResponseDto>> Handle(GetMeetingsOrganizedByUserQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.MeetingRepository.GetMeetingsForUser(request.OrganizerId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meetings.Adapt<IEnumerable<MeetingWithParticipantsResponseDto>>();
    }
}