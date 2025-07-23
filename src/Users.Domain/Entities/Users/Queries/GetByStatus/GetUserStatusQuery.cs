// <copyright file="GetUserStatusQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetStatus;

public class GetUserStatusQueryRequest : IRequest<GetUserStatusQueryResponse>
{
    public Guid Id { get; set; }

    public GetUserStatusQueryRequest(Guid id)
    {
        this.Id = id;
    }
}
