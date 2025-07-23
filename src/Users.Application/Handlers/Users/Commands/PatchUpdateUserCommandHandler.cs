// <copyright file="PatchUpdateUserCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Exceptions;
using Users.Data.Tables;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Commands;

public class PatchUpdateUserCommandHandler : IRequestHandler<PatchUpdateUserCommand, PatchUpdateUserCommandResponse>
{
    private readonly IUsersRepository repository;
    private readonly ILogger<PatchUpdateUserCommandHandler> logger;

    public PatchUpdateUserCommandHandler(
        IUsersRepository repository,
        ILogger<PatchUpdateUserCommandHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PatchUpdateUserCommandResponse> Handle(PatchUpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.GetAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"User with Id {request.Id} not found.");

        var hasChanges = false;
        if (request.FirstName != null && request.FirstName != user.FirstName) { user.FirstName = request.FirstName;
            hasChanges = true; }
        if (request.LastName != null && request.LastName != user.LastName) { user.LastName = request.LastName;
            hasChanges = true; }
        if (request.Language != null && request.Language != user.Language) { user.Language = request.Language;
            hasChanges = true; }
        if (request.PhoneNumber != null && request.PhoneNumber != user.PhoneNumber) { user.PhoneNumber = request.PhoneNumber;
            hasChanges = true; }
        if (request.RegistrationStatus.HasValue && request.RegistrationStatus.Value != user.RegistrationStatus) { user.RegistrationStatus = request.RegistrationStatus.Value;
            hasChanges = true; }
        if (request.IsBlock.HasValue && request.IsBlock.Value != user.IsBlock) { user.IsBlock = request.IsBlock.Value;
            hasChanges = true; }
        if (request.IsAdmin.HasValue && request.IsAdmin.Value != user.IsAdmin) { user.IsAdmin = request.IsAdmin.Value;
            hasChanges = true; }
        if (request.IsSuspicious.HasValue && request.IsSuspicious.Value != user.IsSuspicious) { user.IsSuspicious = request.IsSuspicious.Value;
            hasChanges = true; }
        if (request.IsPremium.HasValue && request.IsPremium.Value != user.IsPremium) { user.IsPremium = request.IsPremium.Value;
            hasChanges = true; }
        if (request.HasVehicle.HasValue && request.HasVehicle.Value != user.HasVehicle) { user.HasVehicle = request.HasVehicle.Value;
            hasChanges = true; }
        if (hasChanges)
        {
            await this.repository.SaveChangesAsync(cancellationToken);
        }

        return new PatchUpdateUserCommandResponse
        {
            Id = user.Id,
            Success = hasChanges,
            Message = hasChanges ? "User updated successfully." : "No changes applied.",
        };
    }

    private object ConvertValue(PropertyInfo propertyInfo, JsonElement jsonElement)
    {
        return propertyInfo.PropertyType switch
        {
            _ when propertyInfo.PropertyType == typeof(bool) => jsonElement.GetBoolean(),
            _ when propertyInfo.PropertyType == typeof(Guid?) => jsonElement.ValueKind == JsonValueKind.Null
                ? null
                : Guid.Parse(jsonElement.GetString() !),
            _ when propertyInfo.PropertyType == typeof(string) => jsonElement.GetString() !,
            _ => throw new InvalidOperationException(
                $"Unsupported type {propertyInfo.PropertyType.Name} for field {propertyInfo.Name}")
        };
    }
}