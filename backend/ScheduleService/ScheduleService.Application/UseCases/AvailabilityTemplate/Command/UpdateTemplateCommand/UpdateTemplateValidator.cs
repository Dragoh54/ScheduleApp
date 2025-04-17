using FluentValidation;
using ScheduleService.Application.Validation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateValidator : AbstractValidator<UpdateTemplateCommand>
{
    public UpdateTemplateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Template name is required")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Schedule)
            .NotEmpty().WithMessage("At least one day must be specified");

        RuleForEach(x => x.Schedule)
            .Must((_, kvp) => kvp.Value != null && kvp.Value.Any())
            .WithMessage("At least one time slot must be specified for each day")
            .OverridePropertyName("Schedule");

        RuleForEach(x => x.Schedule)
            .ChildRules(dayRules =>
            {
                dayRules.RuleForEach(day => day.Value)
                    .SetValidator(new TimeSlotDtoValidator())
                    .OverridePropertyName($"Schedule.{dayRules}");
            });
    }
}