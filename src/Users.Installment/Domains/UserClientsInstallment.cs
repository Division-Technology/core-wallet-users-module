// <copyright file="UserClientsInstallment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Handlers.UserClients.Commands;
using Users.Application.Handlers.UserClients.Commands.Create;
using Users.Application.Handlers.UserClients.Commands.PatchUpdate;
using Users.Application.Handlers.UserClients.Queries;
using Users.Application.Handlers.UserClients.Queries.GetActive;
using Users.Application.Handlers.UserClients.Queries.GetById;
using Users.Application.Handlers.UserClients.Queries.GetByTelegramId;
using Users.Application.Handlers.UserClients.Queries.GetByUser;
using Users.Application.Mappings.UserClients;
using Users.Domain.Entities.UserClients.Commands.Create;
using Users.Domain.Entities.UserClients.Commands.PatchUpdate;
using Users.Domain.Entities.UserClients.Queries.GetActive;
using Users.Domain.Entities.UserClients.Queries.GetById;
using Users.Domain.Entities.UserClients.Queries.GetByTelegramId;
using Users.Domain.Entities.UserClients.Queries.GetByUser;
using Users.Repositories.UserClients;

namespace Users.Installment.Domains;

public static class UserClientsInstallment
{
    public static void InstallUserClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRequestHandler<CreateUserClientCommand, CreateUserClientCommandResponse>, CreateUserClientCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<PatchUpdateUserClientCommand, PatchUpdateUserClientCommandResponse>, PatchUpdateUserClientCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<GetUserClientByIdQuery, GetUserClientByIdQueryResponse>, GetUserClientByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetUserClientByTelegramIdQuery, GetUserClientByTelegramIdQueryResponse>, GetUserClientByTelegramIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetUserClientsQuery, GetUserClientsQueryResponse>, GetUserClientsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetActiveUserClientsQuery, GetActiveUserClientsQueryResponse>, GetActiveUserClientsQueryHandler>();
        builder.Services.AddScoped<IUserClientsRepository, UserClientsRepository>();

        // Register AutoMapper profile if needed
        // builder.Services.AddAutoMapper(typeof(UserClientProfile).Assembly);
    }
}