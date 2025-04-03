using FluentValidation;
using UserService.Application.Dto;

namespace UserService.Application.Validator.UserValidators;

public class AddRoleValidator : AbstractValidator<Dto.AddRoleDto>
{
    public AddRoleValidator()
    {
        RuleFor(user => user)
            .NotNull()
            .WithMessage("User is required.");
        
        RuleFor(user => user.UserId)
            .NotNull()
            .WithMessage("Id is required.");
        
        RuleFor(user => user.Role)
            .NotNull()
            .WithMessage("Role is required.");
    }
}