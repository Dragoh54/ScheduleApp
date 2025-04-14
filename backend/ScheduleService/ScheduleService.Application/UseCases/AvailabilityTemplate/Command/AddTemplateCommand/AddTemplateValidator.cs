using FluentValidation;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Validation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateValidator : AbstractValidator<AddTemplateCommand>
{
    public AddTemplateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        
        RuleFor(x => x.Schedule)
            .NotNull()
            .Must(s => s != null && s.Keys.All(k => Enum.IsDefined(typeof(DayOfWeek), k)))
            .WithMessage("Schedule contains invalid day of week");
            
        RuleForEach(x => x.Schedule)
            .Must(kvp => kvp.Value != null)
            .OverridePropertyName("Schedule")
            .WithMessage("Time slots cannot be null for day {CollectionIndex}");
            
        RuleForEach(x => x.Schedule.Values.SelectMany(v => v))
            .SetValidator(new TimeSlotDtoValidator())
            .OverridePropertyName("Schedule");
    }
}