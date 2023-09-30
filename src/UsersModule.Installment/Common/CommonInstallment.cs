using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersModule.Data;

namespace UsersModule.Installment.Common;

public static class CommonInstallment
{
    public static void InstallCommon(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMapper>(options =>
        {
            var config = AutomapperConfigurations.GetConfigurations;
            return config.CreateMapper();
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<UsersModuleDbContext>(options => options.UseNpgsql(connectionString));

        
    }
}