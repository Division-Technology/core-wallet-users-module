// <copyright file="SetUserBlockedCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS8601 // False positive - IsBlocked is a bool value type, cannot be null

using MediatR;
using Users.Domain.Entities.Users.Commands.SetBlocked;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Application.Exceptions;

namespace Users.Application.Handlers.Users.Commands;

public class SetUserBlockedCommandHandler : IRequestHandler<SetUserBlockedCommand, SetUserBlockedCommandResponse>
{
    private readonly IMediator mediator;
    
    public SetUserBlockedCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task<SetUserBlockedCommandResponse> Handle(SetUserBlockedCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new BadRequestException("UserId is required.");
        var patchCommand = new PatchUpdateUserCommand
        {
            Id = request.UserId, // Use UserId as primary identifier
            IsBlocked = request.IsBlocked  // Only set this field
        };
        
        var result = await mediator.Send(patchCommand, cancellationToken);
        return new SetUserBlockedCommandResponse { Success = result.Success, Message = result.Message };
    }
}