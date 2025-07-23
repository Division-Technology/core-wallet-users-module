// <copyright file="GetUserRegistrationStatusQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.GetRegistrationStatus;

public class GetUserRegistrationStatusQueryResponse
{
    public RegistrationStatus RegistrationStatus { get; set; }
}