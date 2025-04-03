using Mapster;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    ITokenService tokenService
    ) : IUserService
{
    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(id, cancellationToken)
            ?? throw new AlreadyExistsException("User with this id doesn't exist!");;
        
        return candidate.Adapt<UserDto>();
    }
    
    public async Task<PaginatedListDto<UserDto>> GetUsers(PaginatedPageUsers request, CancellationToken cancellationToken)
    {
        var (pageNumber, pageSize) = (request.PageNumber, request.PageSize);
        
        var (items, totalCount) = await unitOfWork.UserRepository.Get(request.UserFilters, pageNumber, pageSize, cancellationToken);

        if (items is null)
        {
            throw new NotFoundException("There are no users!");
        }
        
        var users = items.Adapt<List<UserDto>>();

        return new PaginatedListDto<UserDto>()
        {
            Items = users,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(userDto.Id, cancellationToken)
            ?? throw new AlreadyExistsException("User with this id doesn't exist!");;
        
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
        
        var candidate = await unitOfWork.UserRepository.Get(Guid.Parse(idFromToken), cancellationToken)
                        ?? throw new AlreadyExistsException("User with this id doesn't exist!");
        
        candidate.IsDeleted = true;
        candidate.DeletedAt = DateTime.UtcNow;
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }
}