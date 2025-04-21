using FluentValidation;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusValidator : AbstractValidator<UpdateMeetingStatusCommand>
{
    public UpdateMeetingStatusValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty");
        
        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status cannot be null");
    }
}