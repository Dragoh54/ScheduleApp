using FluentValidation;
using UserService.Application.Dto.UserDtos;

namespace UserService.Application.Validator.UserValidators;

public class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password is must not be empty.")
            .NotNull()
            .WithMessage("Password is required.")
            .MaximumLength(256)
            .WithMessage("Password must not exceed 256 characters.");
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is must not be empty.")
            .NotNull()
            .WithMessage("Email is required.")
            .EmailAddress();
    }
}