using Mapster;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> RegisterUser(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.UserRepository.GetByEmailAsync(registerDto.Email, cancellationToken);

        if (candidate is not null)
        {
            throw new AlreadyExistsException("User with this email already exists!");
        }
        
        var hashedPassword = _passwordHasher.Generate(registerDto.Password, cancellationToken);
        var user = new UserEntity(registerDto.Username, registerDto.Email, hashedPassword, registerDto.FirstName, registerDto.LastName);
        
        await _unitOfWork.UserRepository.Add(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return user.Adapt<UserDto>();
    }

    public async Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> UpdateUser(UserDto userDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> DeleteUser(UserDto userDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}