// <copyright file="UserClientsGrpcService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Users.Api.Grpc;
using Users.Domain.Entities.UserClients.Commands.Create;
using Users.Domain.Entities.UserClients.Commands.PatchUpdate;
using Users.Domain.Entities.UserClients.Queries.GetActive;
using Users.Domain.Entities.UserClients.Queries.GetById;
using Users.Domain.Entities.UserClients.Queries.GetByTelegramId;
using Users.Domain.Entities.UserClients.Queries.GetByUser;

namespace Users.Grpc;

public class UserClientsGrpcService : UserClientsService.UserClientsServiceBase
{
    private readonly IMediator mediator;

    public UserClientsGrpcService(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <inheritdoc/>
    public override async Task<UserClientResponse> CreateClient(CreateUserClientRequest request, ServerCallContext context)
    {
        var command = new CreateUserClientCommand
        {
            UserId = Guid.Parse(request.UserId),
            ChannelType = (Users.Domain.Enums.ChannelType)request.ChannelType,
            TelegramId = request.TelegramId,
            ChatId = request.ChatId,
            DeviceToken = request.DeviceToken,
            SessionId = request.SessionId,
            Platform = request.Platform,
            Version = request.Version,
            Language = request.Language
        };
        var result = await this.mediator.Send(command);
        return new UserClientResponse
        {
            Id = result.Id.ToString(),
            UserId = request.UserId,
            ChannelType = request.ChannelType,
            TelegramId = request.TelegramId,
            ChatId = request.ChatId,
            DeviceToken = request.DeviceToken,
            SessionId = request.SessionId,
            Platform = request.Platform,
            Version = request.Version,
            Language = request.Language,
            IsActive = true, // or map from result if available
            CreatedAt = result.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            LastSeenAt = result.LastSeenAt != null ? result.LastSeenAt.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") : string.Empty
        };
    }

    /// <inheritdoc/>
    public override async Task<UserClientSuccessResponse> UpdateClientLastSeen(UpdateClientLastSeenRequest request, ServerCallContext context)
    {
        // PatchUpdateUserClientCommand can be used for updating last seen
        var command = new PatchUpdateUserClientCommand
        {
            Id = Guid.Parse(request.ClientId),
            LastSeenAt = DateTime.TryParse(request.LastSeenAt, out var dt) ? dt : (DateTime?)null,
        };
        var result = await this.mediator.Send(command);
        return new UserClientSuccessResponse
        {
            Success = result.Success,
            Message = result.Message,
        };
    }

    /// <inheritdoc/>
    public override async Task<UserClientResponse> GetById(GetUserClientByIdRequest request, ServerCallContext context)
    {
        var query = new GetUserClientByIdQuery(Guid.Parse(request.Id));
        var result = await this.mediator.Send(query);
        return new UserClientResponse
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            ChannelType = (Api.Grpc.ChannelType)result.ChannelType,
            TelegramId = result.TelegramId ?? string.Empty,
            ChatId = result.ChatId ?? string.Empty,
            DeviceToken = result.DeviceToken ?? string.Empty,
            SessionId = result.SessionId ?? string.Empty,
            Platform = result.Platform ?? string.Empty,
            Version = result.Version ?? string.Empty,
            Language = result.Language ?? string.Empty,
            IsActive = result.IsActive,
            CreatedAt = result.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            LastSeenAt = result.LastSeenAt?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? string.Empty
        };
    }

    /// <inheritdoc/>
    public override async Task<UserClientResponse> GetByTelegramId(GetUserClientByTelegramIdRequest request, ServerCallContext context)
    {
        var query = new GetUserClientByTelegramIdQuery { TelegramId = request.TelegramId };
        var result = await this.mediator.Send(query);
        return new UserClientResponse
        {
            Id = result.Id.ToString(),
            UserId = result.UserId.ToString(),
            ChannelType = (Api.Grpc.ChannelType)result.ChannelType,
            TelegramId = result.TelegramId ?? string.Empty,
            ChatId = result.ChatId ?? string.Empty,
            DeviceToken = result.DeviceToken ?? string.Empty,
            SessionId = result.SessionId ?? string.Empty,
            Platform = result.Platform ?? string.Empty,
            Version = result.Version ?? string.Empty,
            Language = result.Language ?? string.Empty,
            IsActive = result.IsActive,
            CreatedAt = result.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            LastSeenAt = result.LastSeenAt?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? string.Empty
        };
    }

    /// <inheritdoc/>
    public override async Task<UserClientsResponse> GetByUser(GetUserClientsRequest request, ServerCallContext context)
    {
        var query = new GetUserClientsQuery { UserId = Guid.Parse(request.UserId) };
        var result = await this.mediator.Send(query);
        var response = new UserClientsResponse();
        foreach (var c in result.Clients)
        {
            response.Clients.Add(new UserClientResponse
            {
                Id = c.Id.ToString(),
                UserId = request.UserId,
                ChannelType = (Api.Grpc.ChannelType)c.ChannelType,
                TelegramId = c.TelegramId ?? string.Empty,
                ChatId = c.ChatId ?? string.Empty,
                DeviceToken = c.DeviceToken ?? string.Empty,
                SessionId = c.SessionId ?? string.Empty,
                Platform = c.Platform ?? string.Empty,
                Version = c.Version ?? string.Empty,
                Language = c.Language ?? string.Empty,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                LastSeenAt = c.LastSeenAt?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? string.Empty
            });
        }
        return response;
    }

    /// <inheritdoc/>
    public override async Task<UserClientsResponse> GetActiveByUser(GetActiveUserClientsRequest request, ServerCallContext context)
    {
        var query = new GetActiveUserClientsQuery { UserId = Guid.Parse(request.UserId) };
        var result = await this.mediator.Send(query);
        var response = new UserClientsResponse();
        foreach (var c in result.Clients)
        {
            response.Clients.Add(new UserClientResponse
            {
                Id = c.Id.ToString(),
                UserId = request.UserId,
                ChannelType = (Api.Grpc.ChannelType)c.ChannelType,
                TelegramId = c.TelegramId ?? string.Empty,
                ChatId = c.ChatId ?? string.Empty,
                DeviceToken = c.DeviceToken ?? string.Empty,
                SessionId = c.SessionId ?? string.Empty,
                Platform = c.Platform ?? string.Empty,
                Version = c.Version ?? string.Empty,
                Language = c.Language ?? string.Empty,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                LastSeenAt = c.LastSeenAt?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? string.Empty
            });
        }
        return response;
    }

    /// <inheritdoc/>
    public override async Task<UserClientSuccessResponse> PatchUpdate(PatchUpdateUserClientRequest request, ServerCallContext context)
    {
        var command = new PatchUpdateUserClientCommand
        {
            Id = Guid.Parse(request.Id),
            IsActive = request.IsActive,
            IsBlocked = request.IsBlocked,
            Platform = request.Platform,
            Language = request.Language,
            Version = request.Version,
            LastSeenAt = DateTime.TryParse(request.LastSeenAt, out var dt) ? dt : (DateTime?)null,
            TelegramId = request.TelegramId,
            ChatId = request.ChatId,
            DeviceToken = request.DeviceToken,
            SessionId = request.SessionId
        };
        var result = await this.mediator.Send(command);
        return new UserClientSuccessResponse
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}