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
        if (request.LastSeenAt.HasValue) { client.LastSeenAt = request.LastSeenAt.Value;
            hasChanges = true; }
        if (request.Platform != null) { client.Platform = request.Platform;
            hasChanges = true; }
        if (request.Language != null) { client.Language = request.Language;
            hasChanges = true; }
        if (request.Version != null) { client.Version = request.Version;
            hasChanges = true; }
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