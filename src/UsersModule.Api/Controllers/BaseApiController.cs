using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UsersModule.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;
    
    public BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}