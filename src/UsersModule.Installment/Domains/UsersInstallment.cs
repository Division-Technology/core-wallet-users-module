using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UsersModule.Application.Handlers.Users.Commands;
using UsersModule.Application.Handlers.Users.Queries;
using UsersModule.Domain.Entities.Users.Commands.Requests;
using UsersModule.Domain.Entities.Users.Commands.Responses;
using UsersModule.Domain.Entities.Users.Queries.Requests;
using UsersModule.Domain.Entities.Users.Queries.Responses;
using UsersModule.Repositories.Users;

namespace UsersModule.Installment.Domains;

public static class UsersInstallment
{
    public static void InstallUsers(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRequestHandler<UsersGetQueryRequest, UsersGetQueryResponse>, UsersGetQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<UsersCreateCommandRequest, UsersCreateCommandResponse>, UsersCreateCommandHandler>();

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    }
}