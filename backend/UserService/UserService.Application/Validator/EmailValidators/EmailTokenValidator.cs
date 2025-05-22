using FluentValidation;
using UserService.Application.Dto.EmailDtos;

namespace UserService.Application.Validator.EmailValidators;

public class EmailTokenValidator : AbstractValidator<EmailTokenDto>
{
    public EmailTokenValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Email Token is required");
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is must not be empty.")
            .NotNull()
            .WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(user => user.Token)
            .NotEmpty()
            .WithMessage("Token is must not be empty.")
            .NotNull()
            .WithMessage("Token is required.");
    }
}