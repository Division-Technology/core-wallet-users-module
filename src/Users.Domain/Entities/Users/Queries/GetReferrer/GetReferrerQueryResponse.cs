// <copyright file="GetReferrerQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Users.Domain.Entities.Users.Queries.GetReferrer;

public class GetReferrerQueryResponse
{
    public Guid? ReferrerId { get; set; }

    public string? ReferrerFirstName { get; set; }

    public string? ReferrerLastName { get; set; }

    public string? ReferrerPhoneNumber { get; set; }
}