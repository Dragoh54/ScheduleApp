using FluentValidation;
using UserService.Application.Dto;

namespace UserService.Application.Validator.UserValidators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("Username is must not be empty.")
            .NotNull()
            .WithMessage("Username is required.")
            .MaximumLength(100)
            .WithMessage("Username must not exceed 100 characters.");
        
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is must not be empty.")
            .NotNull()
            .WithMessage("FirstName is required.")
            .MaximumLength(256)
            .WithMessage("FirstName must not exceed 256 characters.");
        
        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage("LastName is must not be empty..")
            .NotNull()
            .WithMessage("LastName is required.")
            .MaximumLength(256)
            .WithMessage("LastName must not exceed 256 characters.");
    }
}