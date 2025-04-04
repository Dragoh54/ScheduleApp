using FluentValidation;
using UserService.Application.Dto.RoleDtos;

namespace UserService.Application.Validator.RoleValidators;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(r => r)
            .NotNull()
            .WithMessage("Role data is required");
        
        RuleFor(r => r.RoleName)
            .NotNull()
            .WithMessage("Role name is required");
    }
}