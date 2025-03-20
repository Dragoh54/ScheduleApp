using FluentValidation;
using UserService.Application.Dto;

namespace UserService.Application.Validator.UserValidators;

public class RegisterUserValidator : AbstractValidator<RegisterDto>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .NotNull()
            .WithMessage("Username is required.")
            .MaximumLength(100)
            .WithMessage("Username must not exceed 100 characters.");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .NotNull()
            .WithMessage("Password is required.")
            .MaximumLength(256)
            .WithMessage("Password must not exceed 256 characters.");
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .NotNull()
            .WithMessage("Email is required.")
            .MaximumLength(100)
            .WithMessage("Email must not exceed 100 characters.")
            .EmailAddress();
        
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.")
            .NotNull()
            .WithMessage("FirstName is required.")
            .MaximumLength(256)
            .WithMessage("FirstName must not exceed 256 characters.");
        
        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.")
            .NotNull()
            .WithMessage("LastName is required.")
            .MaximumLength(256)
            .WithMessage("LastName must not exceed 256 characters.");
    }
}