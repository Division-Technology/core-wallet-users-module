// <copyright file="ClientDataService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Users.Application.Services;

public class ClientDataService : IClientDataService
{
    private readonly JsonSerializerOptions jsonOptions;

    public ClientDataService()
    {
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }

    /// <inheritdoc/>
    public T DeserializeClientData<T>(JsonDocument clientData)
        where T : class
    {
        if (clientData == null)
        {
            throw new ArgumentNullException(nameof(clientData));
        }

        var jsonString = clientData.RootElement.GetRawText();
        return JsonSerializer.Deserialize<T>(jsonString, this.jsonOptions)
            ?? throw new InvalidOperationException($"Failed to deserialize client data to type {typeof(T).Name}");
    }

    /// <inheritdoc/>
    public JsonDocument SerializeClientData<T>(T data)
        where T : class
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var jsonString = JsonSerializer.Serialize(data, this.jsonOptions);
        return JsonDocument.Parse(jsonString);
    }

    /// <inheritdoc/>
    public bool ValidateClientData<T>(JsonDocument clientData)
        where T : class
    {
        if (clientData == null)
        {
            return false;
        }

        try
        {
            var jsonString = clientData.RootElement.GetRawText();
            JsonSerializer.Deserialize<T>(jsonString, this.jsonOptions);
            return true;
        }
        catch
        {
            return false;
        }
    }
}