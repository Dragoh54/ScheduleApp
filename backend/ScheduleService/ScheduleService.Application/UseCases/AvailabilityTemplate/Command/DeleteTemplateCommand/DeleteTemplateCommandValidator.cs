using FluentValidation;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;

public class DeleteTemplateCommandValidator : AbstractValidator<DeleteTemplateCommand>
{
    public DeleteTemplateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Template ID is required");
    }
}