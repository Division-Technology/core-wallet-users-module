// <copyright file="WebAppClientData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.ClientData;

public class WebAppClientData
{
    public string? SessionId { get; set; }

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }
}