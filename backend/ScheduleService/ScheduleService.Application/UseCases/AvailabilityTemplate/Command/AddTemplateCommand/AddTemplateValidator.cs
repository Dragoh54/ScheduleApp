using FluentValidation;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Validation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateValidator : AbstractValidator<AddTemplateCommand>
{
    public AddTemplateValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Template name is required")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Schedule)
            .NotEmpty().WithMessage("At least one day must be specified");

        RuleForEach(x => x.Schedule)
            .ChildRules(day =>
            {
                day.RuleFor(x => x.TimeSlots)
                    .NotEmpty().WithMessage("At least one time slot must be specified for each day");

                day.RuleForEach(x => x.TimeSlots)
                    .SetValidator(new TimeSlotDtoValidator());
            });
    }
}