using MediatR;
using UsersModule.Domain.Entities.Users.Commands.Responses;

namespace UsersModule.Domain.Entities.Users.Commands.Requests;

public class UsersCreateCommandRequest : IRequest<UsersCreateCommandResponse>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}