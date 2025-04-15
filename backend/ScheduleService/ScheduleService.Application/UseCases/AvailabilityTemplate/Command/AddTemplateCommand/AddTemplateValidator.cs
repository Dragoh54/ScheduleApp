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
            
        // Валидация каждого дня в расписании
        RuleForEach(x => x.Schedule)
            .Must((_, kvp) => kvp.Value != null && kvp.Value.Any())
            .WithMessage("At least one time slot must be specified for each day")
            .OverridePropertyName("Schedule");
            
        // Валидация каждого временного слота
        RuleForEach(x => x.Schedule)
            .ChildRules(dayRules =>
            {
                dayRules.RuleForEach(day => day.Value)
                    .SetValidator(new TimeSlotDtoValidator())
                    .OverridePropertyName($"Schedule.{dayRules}");
            });
    }
}