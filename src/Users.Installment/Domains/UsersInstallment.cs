// <copyright file="UsersInstallment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Handlers.Users.Commands;
using Users.Application.Handlers.Users.Queries;
using Users.Application.Handlers.Users.Queries.GetReferrer;
using Users.Application.Handlers.Users.Queries.GetRegistrationStatus;
using Users.Application.Handlers.Users.Queries.GetUser;
using Users.Application.Mappings.Users;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Queries.GetById;
using Users.Domain.Entities.Users.Queries.GetStatus;
using Users.Repositories.Users;

namespace Users.Installment.Domains;

public static class UsersInstallment
{
    public static void InstallUsers(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<
            MediatR.IRequestHandler<
                Users.Domain.Entities.Users.Queries.GetRegistrationStatus.GetUserRegistrationStatusQuery,
                Users.Domain.Entities.Users.Queries.GetRegistrationStatus.GetUserRegistrationStatusQueryResponse>,
            Users.Application.Handlers.Users.Queries.GetRegistrationStatus.GetUserRegistrationStatusQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>, CreateUserCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<PatchUpdateUserCommand, PatchUpdateUserCommandResponse>, PatchUpdateUserCommandHandler>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    }
}
