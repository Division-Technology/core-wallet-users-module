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
            this.CreateMap<CreateUserCommandRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            this.CreateMap<User, CreateUserCommandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RegistrationStatus, opt => opt.MapFrom(src => src.RegistrationStatus));

            this.CreateMap<User, GetUserStatusQueryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RegistrationStatus));

            this.CreateMap<User, Q.GetUserByIdQueryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.RegistrationStatus, opt => opt.MapFrom(src => src.RegistrationStatus))
                .ForMember(dest => dest.HasVehicle, opt => opt.MapFrom(src => src.HasVehicle))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt));

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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Success, opt => opt.Ignore())
                .ForMember(dest => dest.Message, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.RegistrationStatus, opt => opt.MapFrom(src => src.RegistrationStatus))
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.HasVehicle, opt => opt.MapFrom(src => src.HasVehicle))
                .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}