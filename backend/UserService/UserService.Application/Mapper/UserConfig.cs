using Mapster;
using UserService.Application.Dto.RoleDtos;
using UserService.Application.Dto.UserDtos;
using UserService.DataAccess.Models;

namespace UserService.Application.Mapper;

public static class UserConfig
{
    public static void RegisterMappings()
    {
        UserEntityToUserDtoMapping();
        UserDtoToUserEntityMapping();
        
        UpdateUserDtoToUserEntityMapping();
        UserEntityToUpdateUserDtoMapping();
    }

    private static void UserEntityToUserDtoMapping()
    {
        TypeAdapterConfig<UserEntity, UserDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.LastLoginAt, src => src.LastLoginAt)
            .Map(dest => dest.Roles, src => 
                src.UserRoles
                    .Select(ur => ur.Role.Adapt<RoleDto>())
                    .ToList());
    }

    private static void UserDtoToUserEntityMapping()
    {
        TypeAdapterConfig<UserDto, UserEntity>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Ignore(dest => dest.PasswordHash)
            .Ignore(dest => dest.Email)            
            .Ignore(dest => dest.UserRoles);
    }

    private static void UpdateUserDtoToUserEntityMapping()
    {
        TypeAdapterConfig<UserEntity, UpdateUserDto>
            .NewConfig()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);
    }

    private static void UserEntityToUpdateUserDtoMapping()
    {
        TypeAdapterConfig<UpdateUserDto, UserEntity>
            .NewConfig()
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
            .Ignore(dest => dest.Email) 
            .Ignore(dest => dest.PasswordHash); 
    }
}