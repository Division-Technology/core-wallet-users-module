// <copyright file="UsersController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Domain.Entities.Users.Queries.GetReferrer;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Domain.Entities.Users.Queries.GetUser;
using Users.Domain.Entities.Users.Queries.ListUsers;

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

    [HttpGet("{id:guid}/referrer")]
    [ProducesResponseType(typeof(GetReferrerQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReferrerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await this.Mediator.Send(new GetReferrerQuery { UserId = id }, cancellationToken);
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
