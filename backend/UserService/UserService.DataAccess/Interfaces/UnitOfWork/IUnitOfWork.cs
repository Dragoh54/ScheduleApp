namespace UserService.DataAccess.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ITokenModelRepository TokenModelRepository { get; }
    IRoleRepository RoleRepository { get; }
    Task SaveChangesAsync();
}