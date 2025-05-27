using FluentValidation;
using ScheduleService.Application.Validation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Template name is required")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Schedule)
            .NotEmpty().WithMessage("At least one day must be specified");

        RuleForEach(x => x.Schedule)
            .ChildRules(dayRules =>
            {
                dayRules.RuleFor(day => day.TimeSlots)
                    .NotEmpty().WithMessage("At least one time slot must be specified for each day");
                dayRules.RuleForEach(day => day.TimeSlots)
                    .SetValidator(new TimeSlotDtoValidator());
            });
    }
}