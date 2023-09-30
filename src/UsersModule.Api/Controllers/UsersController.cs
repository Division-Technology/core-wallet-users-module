using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersModule.Domain.Entities.Users.Commands.Requests;
using UsersModule.Domain.Entities.Users.Commands.Responses;
using UsersModule.Domain.Entities.Users.Queries.Requests;
using UsersModule.Domain.Entities.Users.Queries.Responses;

namespace UsersModule.Controllers;

public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("{id:long}")]
    public async Task<UsersGetQueryResponse> GetAsync(long id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new UsersGetQueryRequest(id), cancellationToken);

    [HttpPost]
    public async Task<UsersCreateCommandResponse> CreateAsync(UsersCreateCommandRequest request,
        CancellationToken cancellationToken = default)
        => await Mediator.Send(request, cancellationToken);
}