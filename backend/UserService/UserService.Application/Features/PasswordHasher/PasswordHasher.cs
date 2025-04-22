using UserService.Application.Interfaces.Auth;

namespace UserService.Application.Features.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password, CancellationToken cancellationToken)
    {
        var token = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }

    public bool Verify(string password, string? hashedPassword, CancellationToken cancellationToken)
    {
        if (hashedPassword is null)
        {
            return false;
        }
        
        var success = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        cancellationToken.ThrowIfCancellationRequested();
        
        return success;
    }
}