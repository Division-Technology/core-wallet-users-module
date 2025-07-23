// <copyright file="GetUserClientByTelegramIdQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using MediatR;

namespace Users.Domain.Entities.UserClients.Queries.GetByTelegramId;

public class GetUserClientByTelegramIdQuery : IRequest<GetUserClientByTelegramIdQueryResponse>
{
    public string? TelegramId { get; set; }
}