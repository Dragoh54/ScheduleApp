using Mapster;
using UserService.DataAccess.Models;

namespace UserService.Grpc.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserEntity, GetUserInfoReply>();
    }
}