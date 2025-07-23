// <copyright file="GetUserStatusQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.GetStatus;

public class GetUserStatusQueryResponse
{
    public Guid Id { get; set; }

    public RegistrationStatus Status { get; set; }

    public GetUserStatusQueryResponse(Guid id)
    {
        this.Id = id;
    }
}
