using Mapster;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class UserService(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IJwtProvider jwtProvider) : IUserService
{
    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(id, cancellationToken);
        if (candidate is null)
        {
            throw new AlreadyExistsException("User with this id doesn't exist!");
        }
        
        return candidate.Adapt<UserDto>();
    }

    public async Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);

        if (candidate is null)
        {
            throw new AlreadyExistsException("User with this email doesn't exist!");
        }
        
        return candidate.Adapt<UserDto>();
    }
    

    public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
    {
        var candidates = await unitOfWork.UserRepository.Get(cancellationToken)
            ?? throw new NotFoundException("Users not found!");
        
        if (!candidates.Any())
        {
            throw new NotFoundException("No users found");
        }
        
        return candidates.Adapt<IEnumerable<UserDto>>();
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(userDto.Id, cancellationToken);
        if (candidate is null)
        {
            throw new AlreadyExistsException("User with this id doesn't exist!");
        }
        
        userDto.Adapt(candidate);
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return candidate.Adapt<UserDto>();
    }

    public async Task<UserDto> DeleteUser(DeleteUserDto userDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(userDto.Id, cancellationToken);
        if (candidate is null)
        {
            throw new AlreadyExistsException("User with this id doesn't exist!");
        }
        
        await unitOfWork.UserRepository.Delete(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }

    public async Task<UserDto> SoftDelete(DeleteUserDto userDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.Get(userDto.Id, cancellationToken);
        if (candidate is null)
        {
            throw new AlreadyExistsException("User with this id doesn't exist!");
        }
        
        candidate.IsDeleted = true;
        candidate.DeletedAt = DateTime.UtcNow;
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }
}