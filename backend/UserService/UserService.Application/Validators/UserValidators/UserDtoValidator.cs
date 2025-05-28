using FluentValidation;
using UserService.Application.Dto.UserDtos;

namespace UserService.Application.Validators.UserValidators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.Id)
            .NotNull()
            .WithMessage("Id is required.");
        
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("Username is must not be empty.")
            .NotNull()
            .WithMessage("Username is required.")
            .MaximumLength(100)
            .WithMessage("Username must not exceed 100 characters.");
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is must not be empty.")
            .NotNull()
            .WithMessage("Email is required.")
            .MaximumLength(100)
            .WithMessage("Email must not exceed 100 characters.")
            .EmailAddress();
        
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
        
        RuleFor(user => user.Roles)
            .NotNull()
            .WithMessage("Roles is required.");
    }
}