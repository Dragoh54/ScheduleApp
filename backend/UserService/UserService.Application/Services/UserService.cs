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
        var candidates = await unitOfWork.UserRepository.GetAll(cancellationToken);
        if (!candidates.Any())
        {
            throw new NotFoundException("No users found");
        }
        
        return candidates.Adapt<IEnumerable<UserDto>>();
    }

    public async Task<UserDto> RegisterUser(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.GetByEmailAsync(registerDto.Email, cancellationToken);

        if (candidate is not null)
        {
            throw new AlreadyExistsException("User with this email already exists!");
        }
        
        var hashedPassword = passwordHasher.Generate(registerDto.Password, cancellationToken);
        var user = new UserEntity(registerDto.Username, registerDto.Email, hashedPassword, registerDto.FirstName, registerDto.LastName);
        
        await unitOfWork.UserRepository.Add(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return user.Adapt<UserDto>();
    }

    public async Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var userByEmail = await unitOfWork.UserRepository.GetByEmailAsync(loginUserDto.Email, cancellationToken);

        if (userByEmail is null)
        {
            throw new NotFoundException("Cannot found user with this email");
        }

        var result = passwordHasher.Verify(loginUserDto.Password, userByEmail.PasswordHash, cancellationToken);

        if (!result)
        {
            throw new BadRequestException("Failed to login");
        }

        var token = jwtProvider.GenerateAccessToken(userByEmail, cancellationToken);
        var refreshToken = jwtProvider.GenerateRefreshToken(userByEmail, cancellationToken);
        
        if (refreshToken is null || token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate tokens.");
        }
        
        await unitOfWork.RefreshTokenRepository.Add(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return (token, refreshToken.Id.ToString());
    }

    public async Task<bool> Logout(string token, CancellationToken cancellationToken)
    {
        var refreshToken = unitOfWork.RefreshTokenRepository.Get(Guid.Parse(token), cancellationToken).Result;
        cancellationToken.ThrowIfCancellationRequested();

        if (refreshToken is null)
        {
            throw new NotFoundException("Refresh token not found");
        }

        refreshToken.IsUsed = true;

        await unitOfWork.RefreshTokenRepository.Update(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return true;
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
        candidate.DeletedAt = DateTime.Now;
        
        await unitOfWork.UserRepository.Update(candidate, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return candidate.Adapt<UserDto>();
    }
}