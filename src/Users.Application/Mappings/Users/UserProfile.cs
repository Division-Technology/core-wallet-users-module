// <copyright file="UserProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Users.Data.Tables;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Queries.GetStatus;
using Q = Users.Domain.Entities.Users.Queries.GetById;

namespace Users.Application.Mappings.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<CreateUserCommandRequest, User>();
            this.CreateMap<User, CreateUserCommandResponse>();
            this.CreateMap<User, GetUserStatusQueryResponse>();
            this.CreateMap<User, Q.GetUserByIdQueryResponse>();

            // Consolidated PatchUpdate mapping
            this.CreateMap<PatchUpdateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            this.CreateMap<User, PatchUpdateUserCommandResponse>()
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}