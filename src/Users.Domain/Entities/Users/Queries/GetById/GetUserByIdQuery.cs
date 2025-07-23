// <copyright file="GetUserByIdQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetById;

public class GetUserByIdQueryRequest : IRequest<GetUserByIdQueryResponse>
{
    public Guid Id { get; set; }

    public GetUserByIdQueryRequest(Guid id)
    {
        this.Id = id;
    }
}
