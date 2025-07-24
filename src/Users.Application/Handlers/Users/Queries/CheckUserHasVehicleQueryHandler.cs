// <copyright file="CheckUserHasVehicleQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries;

public class CheckUserHasVehicleQueryHandler : IRequestHandler<CheckUserHasVehicleQuery, CheckUserHasVehicleQueryResponse>
{
    private readonly IUsersRepository usersRepository;

    public CheckUserHasVehicleQueryHandler(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public async Task<CheckUserHasVehicleQueryResponse> Handle(CheckUserHasVehicleQuery request, CancellationToken cancellationToken)
    {
        // Try to find user by UserId first
        if (request.UserId.HasValue)
        {
            var user = await usersRepository.GetByIdAsync(request.UserId.Value);
            if (user != null)
            {
                return new CheckUserHasVehicleQueryResponse
                {
                    HasVehicle = user.HasVehicle,
                    UserExists = true,
                    UserId = user.Id,
                    FoundBy = "UserId"
                };
            }
        }

        // Try to find user by TelegramId
        if (request.TelegramId.HasValue)
        {
            var user = await usersRepository.GetByTelegramIdAsync(request.TelegramId.Value);
            if (user != null)
            {
                return new CheckUserHasVehicleQueryResponse
                {
                    HasVehicle = user.HasVehicle,
                    UserExists = true,
                    UserId = user.Id,
                    FoundBy = "TelegramId"
                };
            }
        }

        // User not found
        return new CheckUserHasVehicleQueryResponse
        {
            HasVehicle = false,
            UserExists = false,
            UserId = null,
            FoundBy = string.Empty
        };
    }
} 