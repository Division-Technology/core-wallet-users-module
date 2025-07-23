// <copyright file="UsersGrpcService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;
using Grpc.Core;
using MediatR;
using Users.Api.Grpc;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Domain.Entities.Users.Queries.GetById;
using Users.Domain.Entities.Users.Queries.GetByPhone;
using Users.Domain.Entities.Users.Queries.GetReferrer;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Domain.Enums;
using Users.Repositories.Users;

namespace Users.Grpc;

public class UsersGrpcService : UsersService.UsersServiceBase
{
    private readonly IMediator mediator;
    private readonly IUsersRepository usersRepository;

    public UsersGrpcService(IMediator mediator, IUsersRepository usersRepository)
    {
        this.mediator = mediator;
        this.usersRepository = usersRepository;
    }

    /// <inheritdoc/>
    public override async Task<UserResponse> Create(CreateUserRequest request, ServerCallContext context)
    {
        var command = new CreateUserCommandRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Language = request.Language,
            PhoneNumber = request.PhoneNumber,
            RegistrationStatus = RegistrationStatus.Unregistered, // or map from request if available
            IsBlocked = request.IsBlocked,
            HasVehicle = request.HasVehicle,
            TelegramId = !string.IsNullOrEmpty(request.TelegramId) ? long.Parse(request.TelegramId) : null,
            ChatId = !string.IsNullOrEmpty(request.ChatId) ? long.Parse(request.ChatId) : null,
            Username = request.Username,
        };
        var result = await this.mediator.Send(command);
        var user = await this.usersRepository.GetByIdAsync(result.Id);
        if (user == null)
        {
            return new UserResponse();
        }

        return MapToUserResponse(user);
    }

    /// <inheritdoc/>
    public override async Task<UserResponse> GetById(GetUserByIdRequest request, ServerCallContext context)
    {
        var query = new GetUserByIdQueryRequest(Guid.Parse(request.Id));
        var result = await this.mediator.Send(query);
        return MapToUserResponse(result);
    }

    /// <inheritdoc/>
    public override async Task<UserResponse> GetByPhone(GetUserByPhoneRequest request, ServerCallContext context)
    {
        var query = new GetUserByPhoneQuery(request.PhoneNumber);
        var result = await this.mediator.Send(query);
        return MapToUserResponse(result);
    }

    /// <inheritdoc/>
    public override async Task<UserExistsResponse> Exists(UserExistsRequest request, ServerCallContext context)
    {
        var telegramId = !string.IsNullOrEmpty(request.TelegramId) ? request.TelegramId : null;
        var chatId = !string.IsNullOrEmpty(request.ChatId) ? request.ChatId : null;
        var phoneNumber = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : null;
        var userId = !string.IsNullOrEmpty(request.UserId) ? request.UserId : null;
        var query = new UserExistsQuery(telegramId, phoneNumber, chatId, userId);
        var result = await this.mediator.Send(query);
        return new UserExistsResponse
        {
            Exists = result.Exists,
            UserId = result.UserId?.ToString() ?? string.Empty,
            FoundBy = result.FoundBy ?? string.Empty,
        };
    }

    /// <inheritdoc/>
    public override async Task<GetUserRegistrationStatusResponse> GetRegistrationStatus(GetUserRegistrationStatusRequest request, ServerCallContext context)
    {
        var query = new GetUserRegistrationStatusQuery { UserId = Guid.Parse(request.UserId) };
        var result = await this.mediator.Send(query);
        return new GetUserRegistrationStatusResponse { Status = result.RegistrationStatus.ToString() };
    }

    /// <inheritdoc/>
    public override async Task<GetReferrerResponse> GetReferrer(GetReferrerRequest request, ServerCallContext context)
    {
        var query = new GetReferrerQuery { UserId = Guid.Parse(request.UserId) };
        var result = await this.mediator.Send(query);
        return new GetReferrerResponse { ReferrerId = result.ReferrerId?.ToString() ?? string.Empty };
    }

    /// <inheritdoc/>
    public override async Task<ListUsersResponse> ListUsers(ListUsersRequest request, ServerCallContext context)
    {
        var query = new ListUsersQuery { Page = request.Page, PageSize = request.PageSize };
        var result = await this.mediator.Send(query);
        var response = new ListUsersResponse { Total = result.TotalCount };
        foreach (var u in result.Users)
        {
            response.Users.Add(new UserResponse
            {
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber ?? string.Empty,
                IsBlocked = u.IsBlock,
                HasVehicle = u.HasVehicle,
                CreatedAt = u.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),

                // Only map available properties
            });
        }

        return response;
    }

    /// <inheritdoc/>
    public override async Task<SuccessResponse> PatchUpdate(PatchUpdateUserRequest request, ServerCallContext context)
    {
        var command = new PatchUpdateUserCommand
        {
            Id = Guid.Parse(request.Id),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Language = request.Language,
            PhoneNumber = request.PhoneNumber,
            IsBlocked = request.IsBlocked, // Map is_blocked to IsBlock
            HasVehicle = request.HasVehicle,
        };
        var result = await this.mediator.Send(command);
        return new SuccessResponse { Success = result.Success, Message = result.Message };
    }

    private static UserResponse MapToUserResponse(dynamic user)
    {
        if (user == null)
        {
            return new UserResponse();
        }

        return new UserResponse
        {
            Id = user.Id != null ? user.Id.ToString() : string.Empty,
            TelegramId = string.Empty, // string
            ChatId = string.Empty, // string
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            Username = string.Empty, // not present in User entity
            Language = user.Language ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            IsBlocked = false,
            HasVehicle = user.HasVehicle,
            CreatedAt = user.CreatedAt != null ? user.CreatedAt.ToString("O") : string.Empty,
            ModifiedAt = user.ModifiedAt != null ? user.ModifiedAt.ToString("O") : string.Empty,
        };
    }
}
