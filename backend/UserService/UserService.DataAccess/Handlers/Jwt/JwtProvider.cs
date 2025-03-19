using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Handlers.Jwt;

public class JwtProvider(IConfiguration configuration, IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    private readonly string _secretKey = configuration["JWTSecretKey"] ?? throw new NullReferenceException();
    
    public string GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        List<Claim> claims=
        [
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        ];

        foreach (var role in user.UserRoles)
        {
            claims.Add(new (ClaimTypes.Role, role.Role.RoleName.ToString()));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddMinutes(_jwtOptions.ExpiresMinutes)
        );
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        cancellationToken.ThrowIfCancellationRequested();
        
        return tokenValue;
    }

    public RefreshToken GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = new RefreshToken(Guid.NewGuid(), user.Id,
            DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(_jwtOptions.ExpiresDays));
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
        
    }
}