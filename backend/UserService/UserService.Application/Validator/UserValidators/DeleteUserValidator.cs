using FluentValidation;
using UserService.Application.Dto;

namespace UserService.Application.Validator.UserValidators;

public class DeleteUserValidator : AbstractValidator<DeleteUserDto>
{
    public DeleteUserValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.Id)
            .NotNull()
            .WithMessage("Id is required.");
    }
}