// <copyright file="CreateUserCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using MediatR;
using Users.Data.Tables;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Commands;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUsersRepository repository;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(IUsersRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<CreateUserCommandResponse> Handle(
        CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = this.mapper.Map<User>(request);

        await this.repository.AddAndSaveAsync(user);

        return this.mapper.Map<CreateUserCommandResponse>(user);
    }
}
