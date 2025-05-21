using FluentValidation;
using UserService.Application.Dto.RoleDtos;

namespace UserService.Application.Validators.RoleValidators;

public class AddRoleValidator : AbstractValidator<AddRoleDto>
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