using Mapster;
using UserService.Application.Dto.RoleDto;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.Application.Mapper;

public class RoleConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<RoleDto, RoleEntity>.NewConfig()
            .Map(dest => dest.RoleName, src => src.RoleName);
    }
}