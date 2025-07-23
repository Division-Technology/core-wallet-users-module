// <copyright file="GetUserRegistrationStatusQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries.GetRegistrationStatus;

public class GetUserRegistrationStatusQueryHandler : IRequestHandler<GetUserRegistrationStatusQuery, GetUserRegistrationStatusQueryResponse>
{
    private readonly IUsersRepository repository;

    public GetUserRegistrationStatusQueryHandler(IUsersRepository repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task<GetUserRegistrationStatusQueryResponse> Handle(GetUserRegistrationStatusQuery request, CancellationToken cancellationToken)
    {
        var user = await this.repository.GetAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new NotFoundException($"User {request.UserId} not found.");
        return new GetUserRegistrationStatusQueryResponse { RegistrationStatus = user.RegistrationStatus };
    }
}