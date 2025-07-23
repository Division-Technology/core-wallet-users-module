// <copyright file="MobileAppClientData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Domain.Entities.ClientData;

public class MobileAppClientData
{
    public string? DeviceToken { get; set; }

    public string? AppVersion { get; set; }

    public string? DeviceId { get; set; }

    public string? Platform { get; set; } // "android", "ios"
}