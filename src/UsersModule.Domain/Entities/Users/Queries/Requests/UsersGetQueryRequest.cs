using MediatR;
using UsersModule.Domain.Entities.Users.Queries.Responses;

namespace UsersModule.Domain.Entities.Users.Queries.Requests;

public class UsersGetQueryRequest : IRequest<UsersGetQueryResponse>
{
    public long Id { get; set; }

    public UsersGetQueryRequest(long id)
        => Id = id;
}