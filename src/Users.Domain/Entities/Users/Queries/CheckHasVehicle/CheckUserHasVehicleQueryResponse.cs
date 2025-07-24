// <copyright file="CheckUserHasVehicleQueryResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.Users.Queries.CheckHasVehicle;

public class CheckUserHasVehicleQueryResponse
{
    public bool HasVehicle { get; set; }
    public bool UserExists { get; set; }
    public Guid? UserId { get; set; }
    public string FoundBy { get; set; } = string.Empty;
} 