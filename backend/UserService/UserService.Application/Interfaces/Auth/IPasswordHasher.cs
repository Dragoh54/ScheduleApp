namespace UserService.DataAccess.Interfaces.Auth;

public interface IPasswordHasher
{
    string Generate(string password, CancellationToken cancellationToken);
    bool Verify(string password, string hashedPassword, CancellationToken cancellationToken);
}