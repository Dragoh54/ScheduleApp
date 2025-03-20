using Mapster;
using UserService.Application.Dto;
using UserService.DataAccess.Models;

namespace UserService.Application.Mapper;

public class UserDtoConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<UserDto, UserEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);

        TypeAdapterConfig<UserEntity, UpdateUserDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);
        
        TypeAdapterConfig<UpdateUserDto, UserEntity>
            .NewConfig()
            .Ignore(dest => dest.Email) 
            .Ignore(dest => dest.PasswordHash); 
    }
}