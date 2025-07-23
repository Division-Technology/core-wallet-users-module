// <copyright file="UserClientProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Users.Application.Mappings.UserClients
{
    using AutoMapper;
    using Users.Data.Tables;
    using Users.Domain.Entities.UserClients.Commands.Create;
    using Users.Domain.Entities.UserClients.Queries.GetById;

    public class UserClientProfile : Profile
    {
        public UserClientProfile()
        {
            this.CreateMap<UserClient, GetUserClientByIdQueryResponse>();
            this.CreateMap<CreateUserClientCommand, UserClient>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastSeenAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        }
    }
}