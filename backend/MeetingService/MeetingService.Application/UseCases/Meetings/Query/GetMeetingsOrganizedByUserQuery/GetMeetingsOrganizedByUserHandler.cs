using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public class GetMeetingsOrganizedByUserHandler : IRequestHandler<GetMeetingsOrganizedByUserQuery, IEnumerable<MeetingWithParticipantsDto>>
{
    public GetMeetingsOrganizedByUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<IEnumerable<MeetingWithParticipantsDto>> Handle(GetMeetingsOrganizedByUserQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.MeetingRepository.GetMeetingsForUser(request.OrganizerId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meetings.Adapt<IEnumerable<MeetingWithParticipantsDto>>();
    }
}