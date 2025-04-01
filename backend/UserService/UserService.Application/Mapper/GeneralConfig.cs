namespace UserService.Application.Mapper;

public class GeneralConfig
{
    public static void RegisterMappers()
    {
        RoleConfig.RegisterMappings();
        UserConfig.RegisterMappings();
    }
}