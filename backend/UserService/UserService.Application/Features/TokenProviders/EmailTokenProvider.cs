using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Features.Jwt;
using UserService.Application.Interfaces.Providers;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Models;

namespace UserService.Application.Features.Providers;

public class EmailTokenProvider(
        IConfiguration configuration, 
        IOptions<JwtOptions> jwtOptions
    ) : IEmailTokenProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly string _secretKey = configuration["JWTSecretKey"] ?? throw new NullReferenceException();
    
    public TokenEntity GenerateTokenEntity(UserEntity user, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var token = new TokenEntity()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(JwtConfig.Base64)),
            TokenType = tokenType,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = GetExpirationDate(tokenType),
            IsUsed = false,
        };
        
        cancellationToken.ThrowIfCancellationRequested();

        return token;
    }

    public string GenerateEmailToken(UserEntity user, TokenTypes tokenTypes, CancellationToken cancellationToken)
     {
         List<Claim> claims=
         [
             new(ClaimTypes.Email, user.Email),
         ];
         
         claims.AddRange(user.UserRoles.Select(
             role => new Claim(ClaimTypes.Role, role.Role.RoleName.ToString()))
         );

         var signingCredentials = new SigningCredentials(
             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
             SecurityAlgorithms.HmacSha256);

         var token = new JwtSecurityToken(
             claims: claims,
             signingCredentials: signingCredentials,
             expires: GetExpirationDate(tokenTypes)
         );
         
         var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
         cancellationToken.ThrowIfCancellationRequested();
         
         return tokenValue;
     }

    public int GetTokenExistingTime(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.EmailConfirmation => _jwtOptions.EmailConfirmationExpiresHours,
            TokenTypes.ResetPassword => _jwtOptions.EmailConfirmationExpiresHours,
            TokenTypes.RecoverAccount => _jwtOptions.RecoverAccountExpiresHours,
            _ => throw new BadRequestException("Invalid token type")
        };
    }

    private DateTime GetExpirationDate(TokenTypes tokenType)
    {
        return tokenType switch
        {
             TokenTypes.EmailConfirmation => DateTime.UtcNow.AddHours(_jwtOptions.EmailConfirmationExpiresHours),
             TokenTypes.ResetPassword => DateTime.UtcNow.AddHours(_jwtOptions.EmailConfirmationExpiresHours),
             TokenTypes.RecoverAccount => DateTime.UtcNow.AddHours(_jwtOptions.RecoverAccountExpiresHours),
             _ => throw new BadRequestException("Invalid token type")
        };
    }
}