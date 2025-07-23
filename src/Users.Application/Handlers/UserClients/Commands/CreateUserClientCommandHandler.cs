// <copyright file="CreateUserClientCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Data.Tables;
using Users.Domain.Entities.UserClients.Commands.Create;
using Users.Repositories.UserClients;

namespace Users.Application.Handlers.UserClients.Commands.Create;

public class CreateUserClientCommandHandler : IRequestHandler<CreateUserClientCommand, CreateUserClientCommandResponse>
{
    private readonly IUserClientsRepository repository;
    private readonly IMapper mapper;

    public CreateUserClientCommandHandler(IUserClientsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<CreateUserClientCommandResponse> Handle(CreateUserClientCommand request, CancellationToken cancellationToken)
    {
        // Upsert logic: check if exists by UserId+Type, else create
        var existing = await this.repository.GetByUserIdAndTypeAsync(request.UserId, request.ChannelType, cancellationToken);
        if (existing != null)
        {
            existing.TelegramId = request.TelegramId;
            existing.ChatId = request.ChatId;
            existing.DeviceToken = request.DeviceToken;
            existing.SessionId = request.SessionId;
            existing.Platform = request.Platform;
            existing.Version = request.Version;
            existing.Language = request.Language;
            existing.LastSeenAt = DateTime.UtcNow;
            this.repository.Update(existing);
            await this.repository.SaveChangesAsync(cancellationToken);
            return new CreateUserClientCommandResponse
            {
                Id = existing.Id,
                IsNew = false,
                CreatedAt = existing.CreatedAt,
                LastSeenAt = existing.LastSeenAt,
            };
        }

        var entity = this.mapper.Map<UserClient>(request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.LastSeenAt = DateTime.UtcNow;
        entity.IsActive = true;
        await this.repository.AddAsync(entity, cancellationToken);
        await this.repository.SaveChangesAsync(cancellationToken);
        return new CreateUserClientCommandResponse
        {
            Id = entity.Id,
            IsNew = true,
            CreatedAt = entity.CreatedAt,
            LastSeenAt = entity.LastSeenAt,
        };
    }
}