// <copyright file="UserClientsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Entities.UserClients.Commands.Create;
using Users.Domain.Entities.UserClients.Commands.PatchUpdate;
using Users.Domain.Entities.UserClients.Queries.GetActive;
using Users.Domain.Entities.UserClients.Queries.GetById;
using Users.Domain.Entities.UserClients.Queries.GetByTelegramId;
using Users.Domain.Entities.UserClients.Queries.GetByUser;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/user-clients")]
public class UserClientsController : ControllerBase
{
    private readonly IMediator mediator;

    public UserClientsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserClientCommand command, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(command, cancellationToken);
        return this.CreatedAtAction(nameof(this.GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PatchUpdateUserClientCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await this.mediator.Send(command, cancellationToken);
        return this.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetUserClientByIdQuery(id), cancellationToken);
        if (result == null)
        {
            return this.NotFound();
        }

        return this.Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetUserClientsQuery { UserId = userId }, cancellationToken);
        return this.Ok(result);
    }

    [HttpGet("telegram/{telegramId}")]
    public async Task<IActionResult> GetByTelegramId(string telegramId, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetUserClientByTelegramIdQuery { TelegramId = telegramId }, cancellationToken);
        if (result == null)
        {
            return this.NotFound();
        }

        return this.Ok(result);
    }

    [HttpGet("active")] // e.g. api/user-clients/active?userId=...
    public async Task<IActionResult> GetActive([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetActiveUserClientsQuery { UserId = userId }, cancellationToken);
        return this.Ok(result);
    }
}