using Mapster;
using UserService.Application.Dto.RoleDtos;
using UserService.Application.Dto.UserDtos;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;
using UserService.DataAccess.Models.Pagination;

namespace UserService.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    ITokenService tokenService
    ) : IUserService
{
    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.GetById(id, cancellationToken)
            ?? throw new NotFoundException("User with this id doesn't exist!");
        
        return candidate.Adapt<UserDto>();
    }
    
    public async Task<PaginatedListDto<UserDto>> GetUsers(PaginatedPageUsers request, CancellationToken cancellationToken)
    {
        var (pageNumber, pageSize) = (request.PageNumber, request.PageSize);
        
        var (users, totalCount) = await unitOfWork.UserRepository.Get(request.UserFilters, pageNumber, pageSize, cancellationToken);

        if (users is null)
        {
            throw new NotFoundException("There are no users!");
        }
        
        var resultUsers = users.Adapt<List<UserDto>>();

        return new PaginatedListDto<UserDto>()
        {
            Items = resultUsers,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<UserDto> UpdateUser(Guid id, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            throw new BadRequestException("Id cannot be empty!");
        }
        
        var candidate = await unitOfWork.UserRepository.GetById(id, cancellationToken)
                        ?? throw new AlreadyExistsException("User with this id doesn't exist!");
        
        userDto.Adapt(candidate);
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        return candidate.Adapt<UserDto>();
    }

    public async Task<UserDto> AddRole(AddRoleDto roleDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.GetWithTracking(roleDto.UserId, cancellationToken)
                        ?? throw new AlreadyExistsException("User with this id doesn't exist!");
    
        if (!candidate.IsConfirmed)
        {
            throw new BadRequestException("User is not confirmed!");
        }
        
        if (candidate.UserRoles.Any(role => role.Role.RoleName == roleDto.Role))
        {
            throw new BadRequestException($"User is already an {roleDto.Role}!");
        }
    
        var role = await unitOfWork.RoleRepository.GetByRole(roleDto.Role, cancellationToken)
                   ?? throw new BadRequestException("Role does not exist!");
        
        candidate.UserRoles.Add(new UserRoles { UserId = candidate.Id, RoleId = role.Id });
        candidate.UpdatedAt = DateTime.UtcNow;
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }

    public async Task<UserDto> SoftDelete(string token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new BadRequestException("Invalid access token");
        }
        
        var idFromToken = await tokenService.GetIdFromToken(token, cancellationToken);
        
        var candidate = await unitOfWork.UserRepository.GetById(Guid.Parse(idFromToken), cancellationToken)
                        ?? throw new NotFoundException("User with this id doesn't exist!");
        
        candidate.IsDeleted = true;
        candidate.DeletedAt = DateTime.UtcNow;
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }
}