using AutoMapper;
using MediatR;
using UsersModule.Domain.Entities.Users.Queries.Requests;
using UsersModule.Domain.Entities.Users.Queries.Responses;
using UsersModule.Repositories.Users;

namespace UsersModule.Application.Handlers.Users.Queries;

public class UsersGetQueryHandler : IRequestHandler<UsersGetQueryRequest, UsersGetQueryResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersGetQueryHandler (IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public async Task<UsersGetQueryResponse> Handle(UsersGetQueryRequest request, CancellationToken cancellationToken)
    {
        var userData = await _usersRepository.GetAsync(user => user.Id == request.Id, cancellationToken);
        var result = _mapper.Map<UsersGetQueryResponse>(userData);
        return result;
    }
}