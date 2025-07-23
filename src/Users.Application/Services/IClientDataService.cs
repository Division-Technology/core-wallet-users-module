// <copyright file="IClientDataService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;

namespace Users.Application.Services;

public interface IClientDataService
{
    T DeserializeClientData<T>(JsonDocument clientData)
        where T : class;

    JsonDocument SerializeClientData<T>(T data)
        where T : class;

    bool ValidateClientData<T>(JsonDocument clientData)
        where T : class;
}