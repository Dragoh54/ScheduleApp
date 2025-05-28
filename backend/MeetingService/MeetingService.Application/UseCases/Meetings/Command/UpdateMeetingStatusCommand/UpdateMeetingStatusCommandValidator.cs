using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusCommandValidator : AbstractValidator<UpdateMeetingStatusCommand>
{
    public UpdateMeetingStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null");
        
        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status cannot be null");
    }
}