// <copyright file="UsersController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Commands.SetLanguage;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;
using Users.Domain.Entities.Users.Commands.SetBlocked;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Domain.Entities.Users.Queries.GetUser;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Domain.Entities.Users.Queries.GetByPhone;
using Users.Domain.Entities.Users.Queries.CheckHasVehicle;

namespace Users.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateUserCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommandRequest request, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(request, cancellationToken);
        return this.CreatedAtAction(nameof(this.GetAsync), new { id = result.Id }, result);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PatchAsync(Guid id, [FromBody] PatchUpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        command.Id = id;
        await this.Mediator.Send(command, cancellationToken);
        return this.NoContent();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetUserQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new GetUserQuery { Id = id }, cancellationToken);
        return this.Ok(result);
    }

    [HttpGet("by-phone/{phoneNumber}")]
    [ProducesResponseType(typeof(GetUserByPhoneQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByPhoneQuery(phoneNumber);
        var result = await this.Mediator.Send(query, cancellationToken);
        
        if (result == null)
        {
            return this.NotFound();
        }
        
        return this.Ok(result);
    }

    [HttpGet("by-telegram/{telegramId:long}")]
    [ProducesResponseType(typeof(GetUserQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default)
    {
        // Use the generic GetAsync method with a predicate for Telegram ID lookup
        var result = await this.Mediator.Send(new GetUserQuery { TelegramId = telegramId }, cancellationToken);
        
        if (result == null)
        {
            return this.NotFound();
        }
        
        return this.Ok(result);
    }

    [HttpGet("by-chat/{chatId:long}")]
    [ProducesResponseType(typeof(GetUserQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByChatIdAsync(long chatId, CancellationToken cancellationToken = default)
    {
        // Use the generic GetAsync method with a predicate for Chat ID lookup
        var result = await this.Mediator.Send(new GetUserQuery { ChatId = chatId }, cancellationToken);
        
        if (result == null)
        {
            return this.NotFound();
        }
        
        return this.Ok(result);
    }

    [HttpGet("has-vehicle")]
    [ProducesResponseType(typeof(CheckUserHasVehicleQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckHasVehicleAsync(
        [FromQuery] Guid? userId = null, 
        [FromQuery] long? telegramId = null, 
        CancellationToken cancellationToken = default)
    {
        var query = new CheckUserHasVehicleQuery(userId, telegramId);
        var result = await this.Mediator.Send(query, cancellationToken);
        return this.Ok(result);
    }

    [HttpPut("{id:guid}/language")]
    [ProducesResponseType(typeof(SetUserLanguageCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetLanguageAsync(Guid id, [FromBody] string language, CancellationToken cancellationToken = default)
    {
        var command = new SetUserLanguageCommand
        {
            UserId = id,
            Language = language
        };
        
        var result = await this.Mediator.Send(command, cancellationToken);
        return this.Ok(result);
    }

    [HttpPut("{id:guid}/phone-number")]
    [ProducesResponseType(typeof(SetUserPhoneNumberCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetPhoneNumberAsync(Guid id, [FromBody] string phoneNumber, CancellationToken cancellationToken = default)
    {
        var command = new SetUserPhoneNumberCommand
        {
            UserId = id,
            NewPhoneNumber = phoneNumber
        };
        
        var result = await this.Mediator.Send(command, cancellationToken);
        return this.Ok(result);
    }

    [HttpPut("{id:guid}/blocked")]
    [ProducesResponseType(typeof(SetUserBlockedCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetBlockedAsync(Guid id, [FromBody] bool isBlocked, CancellationToken cancellationToken = default)
    {
        var command = new SetUserBlockedCommand
        {
            UserId = id,
            IsBlocked = isBlocked
        };
        
        var result = await this.Mediator.Send(command, cancellationToken);
        return this.Ok(result);
    }

    [HttpPut("{id:guid}/has-vehicle")]
    [ProducesResponseType(typeof(SetUserHasVehicleCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetHasVehicleAsync(Guid id, [FromBody] bool hasVehicle, CancellationToken cancellationToken = default)
    {
        var command = new SetUserHasVehicleCommand
        {
            UserId = id,
            HasVehicle = hasVehicle
        };
        
        var result = await this.Mediator.Send(command, cancellationToken);
        return this.Ok(result);
    }

    [HttpPut("{id:guid}/profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfileAsync(Guid id, [FromBody] PatchUpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        command.Id = id;
        await this.Mediator.Send(command, cancellationToken);
        return this.NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListUsersQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new ListUsersQuery { Page = page, PageSize = pageSize }, cancellationToken);
        return this.Ok(result);
    }

    [HttpGet("exists")]
    [ProducesResponseType(typeof(UserExistsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExistsAsync([FromQuery] Guid? id, [FromQuery] string? phoneNumber, CancellationToken cancellationToken = default)
    {
        var query = new UserExistsQuery(null, phoneNumber, null, id?.ToString());
        var result = await this.Mediator.Send(query, cancellationToken);
        return this.Ok(result);
    }

    [HttpGet("{id:guid}/registration-status")]
    [ProducesResponseType(typeof(GetUserRegistrationStatusQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegistrationStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new GetUserRegistrationStatusQuery { UserId = id }, cancellationToken);
        return this.Ok(result);
    }
}
