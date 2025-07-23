// <copyright file="GetReferrerQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Queries.GetReferrer;
using Users.Repositories.Users;

namespace Users.Application.Handlers.Users.Queries.GetReferrer;

public class GetReferrerQueryHandler : IRequestHandler<GetReferrerQuery, GetReferrerQueryResponse>
{
    private readonly IUsersRepository repository;

    public GetReferrerQueryHandler(IUsersRepository repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task<GetReferrerQueryResponse> Handle(GetReferrerQuery request, CancellationToken cancellationToken)
    {
        var user = await this.repository.GetAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new NotFoundException($"User {request.UserId} not found.");
        if (user.Referrer == null)
        {
            return new GetReferrerQueryResponse();
        }

        var referrer = await this.repository.GetAsync(x => x.Id == user.Referrer.Value, cancellationToken);
        return new GetReferrerQueryResponse
        {
            ReferrerId = referrer?.Id,
            ReferrerFirstName = referrer?.FirstName,
            ReferrerLastName = referrer?.LastName,
            ReferrerPhoneNumber = referrer?.PhoneNumber,
        };
    }
}