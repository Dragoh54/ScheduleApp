using FluentValidation;
using UserService.Application.Dto.EmailDtos;

namespace UserService.Application.Validator.EmailValidators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Reset data is required.");

        RuleFor(x => x.Email)
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

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password must not be empty.")
            .NotNull()
            .WithMessage("Password is required.");
        //TODO: FOR FUTURE CHECK LEGIT PASSWORD
            // .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            // .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            // .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            // .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password must not be empty.")
            .NotNull()
            .WithMessage("Confirm password is required.")
            .Equal(x => x.Password) 
            .WithMessage("Passwords do not match.");
    }
}