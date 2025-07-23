// <copyright file="AutomapperConfigurations.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Users.Application.Mappings.Users;

namespace Users.Installment.Common;

public static class AutomapperConfigurations
{
    public static MapperConfiguration GetConfigurations => new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<UserProfile>();
    });
}