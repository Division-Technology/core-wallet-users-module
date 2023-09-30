using AutoMapper;
using MediatR;
using UsersModule.Data.Tables;
using UsersModule.Domain.Entities.Users.Commands.Requests;
using UsersModule.Domain.Entities.Users.Commands.Responses;
using UsersModule.Repositories.Users;

namespace UsersModule.Application.Handlers.Users.Commands;

public class UsersCreateCommandHandler : IRequestHandler<UsersCreateCommandRequest, UsersCreateCommandResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    
    public UsersCreateCommandHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public async Task<UsersCreateCommandResponse> Handle(UsersCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var userData = _mapper.Map<User>(request);
        await _usersRepository.AddAndSaveAsync(userData);
        var result = _mapper.Map<UsersCreateCommandResponse>(userData);
        
        return result;
    }
}