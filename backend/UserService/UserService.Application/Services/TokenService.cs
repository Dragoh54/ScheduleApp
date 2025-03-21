using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.TokenDtos;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.Application.Services;

public class TokenService : IRefreshTokenService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
    {
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<string> GenerateAccessToken(LoginUserDto user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateRefreshToken(LoginUserDto user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TokenDto> UpdateToken(TokenDto token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}