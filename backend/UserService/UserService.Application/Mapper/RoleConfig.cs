using Mapster;
using UserService.Application.Dto.RoleDtos;
using UserService.DataAccess.Models;

namespace UserService.Application.Mapper;

public static class RoleConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<RoleEntity, RoleDto>
            .NewConfig()
            .Map(dest => dest.RoleName, src => src.RoleName);
    }
}