// <copyright file="CheckUserHasVehicleQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.CheckHasVehicle;

public class CheckUserHasVehicleQuery : IRequest<CheckUserHasVehicleQueryResponse>
{
    public Guid? UserId { get; set; }
    public long? TelegramId { get; set; }

    public CheckUserHasVehicleQuery(Guid? userId = null, long? telegramId = null)
    {
        UserId = userId;
        TelegramId = telegramId;
    }
} 