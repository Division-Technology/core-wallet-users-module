using AutoMapper;
using UsersModule.Application.Mappings.Users;

namespace UsersModule.Installment.Common;

internal static class AutomapperConfigurations
{
    internal static MapperConfiguration GetConfigurations => new(cfg =>
    {
        cfg.AddProfile(new UsersProfile());
    });
}