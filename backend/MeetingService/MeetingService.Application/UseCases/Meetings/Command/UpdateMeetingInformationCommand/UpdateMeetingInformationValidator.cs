using FluentValidation;
using MeetingService.DomainModel.Settings;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public class UpdateMeetingInformationValidator : AbstractValidator<UpdateMeetingInformationCommand>
{
    public UpdateMeetingInformationValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null.");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(MeetingSettings.TitleMaxLength)
            .WithMessage($"Title must not exceed {MeetingSettings.TitleMaxLength} characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(MeetingSettings.DescriptionMaxLength)
            .WithMessage($"Description must not exceed {MeetingSettings.DescriptionMaxLength} characters.");
    }
}