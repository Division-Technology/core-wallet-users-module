using AutoMapper;
using UsersModule.Data.Tables;
using UsersModule.Domain.Entities.Users.Commands.Requests;
using UsersModule.Domain.Entities.Users.Commands.Responses;
using UsersModule.Domain.Entities.Users.Queries.Responses;

namespace UsersModule.Application.Mappings.Users;

public class UsersProfile : Profile
{
    public UsersProfile ()
    {
        CreateMap<User, UsersGetQueryResponse>();
        CreateMap<UsersCreateCommandRequest, User>();
        CreateMap<User, UsersCreateCommandResponse>();
    }
}