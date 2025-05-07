using FluentValidation;
using MeetingService.DomainModel.Settings;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public class CreateMeetingValidator : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("AccessToken is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(MeetingSettings.TitleMaxLength)
            .WithMessage($"Title must not exceed {MeetingSettings.TitleMaxLength} characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(MeetingSettings.DescriptionMaxLength)
            .WithMessage($"Description must not exceed {MeetingSettings.DescriptionMaxLength} characters.");

        RuleFor(x => x.StartTime)
            .NotNull().WithMessage("StartTime is required.")
            .LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");

        RuleFor(x => x.EndTime)
            .NotNull().WithMessage("EndTime is required.")
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
    }
}