// <copyright file="PatchUpdateUserClientCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.UserClients.Commands.PatchUpdate;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Commands.PatchUpdate;

public class PatchUpdateUserClientCommandHandler : IRequestHandler<PatchUpdateUserClientCommand, PatchUpdateUserClientCommandResponse>
{
    private readonly IUserClientsRepository repository;

    public PatchUpdateUserClientCommandHandler(IUserClientsRepository repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task<PatchUpdateUserClientCommandResponse> Handle(PatchUpdateUserClientCommand request, CancellationToken cancellationToken)
    {
        var client = await this.repository.GetAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"UserClient {request.Id} not found.");
        var hasChanges = false;
        if (request.IsActive.HasValue && request.IsActive.Value != client.IsActive) { client.IsActive = request.IsActive.Value;
            hasChanges = true; }
        if (request.IsBlocked.HasValue)
        { /* update ClientData JSON for is_blocked */
            hasChanges = true;
        }
        if (request.LastSeenAt.HasValue) { client.LastSeenAt = request.LastSeenAt.Value;
            hasChanges = true; }
        if (request.Platform != null)
        { /* update ClientData JSON for platform */
            hasChanges = true;
        }
        if (request.Language != null)
        { /* update ClientData JSON for language */
            hasChanges = true;
        }
        if (request.Version != null)
        { /* update ClientData JSON for version */
            hasChanges = true;
        }
        if (hasChanges)
        {
            this.repository.Update(client);
            await this.repository.SaveChangesAsync(cancellationToken);
        }

        return new PatchUpdateUserClientCommandResponse
        {
            Id = client.Id,
            Success = hasChanges,
            Message = hasChanges ? "UserClient updated successfully." : "No changes applied.",
        };
    }
}