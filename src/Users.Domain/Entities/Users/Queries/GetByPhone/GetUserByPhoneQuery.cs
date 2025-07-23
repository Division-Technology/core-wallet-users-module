// <copyright file="GetUserByPhoneQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace Users.Domain.Entities.Users.Queries.GetByPhone;

public class GetUserByPhoneQuery : IRequest<GetUserByPhoneQueryResponse>
{
    public string PhoneNumber { get; set; }

    public GetUserByPhoneQuery(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
    }
}