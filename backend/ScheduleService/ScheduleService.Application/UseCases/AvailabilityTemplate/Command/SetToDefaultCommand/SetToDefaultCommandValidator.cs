using FluentValidation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public class SetToDefaultCommandValidator : AbstractValidator<SetToDefaultCommand>
{
    public SetToDefaultCommandValidator()
    {
        RuleFor(x => x.TemplateId)
            .NotEmpty().WithMessage("Template ID is required");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");
    }
}