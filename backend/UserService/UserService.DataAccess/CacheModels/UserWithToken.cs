using UserService.DataAccess.Models;

namespace UserService.DataAccess.RedisModels;

public class UserWithToken(UserEntity user, string token)
{
    public UserEntity User { get; set; } = user;
    public string Token { get; set; } = token;
}