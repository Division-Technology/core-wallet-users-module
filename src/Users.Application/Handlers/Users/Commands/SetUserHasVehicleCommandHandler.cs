// <copyright file="SetUserHasVehicleCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS8601 // False positive - HasVehicle is a bool value type, cannot be null

using MediatR;
using Users.Domain.Entities.Users.Commands.SetHasVehicle;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Application.Exceptions;

namespace Users.Application.Handlers.Users.Commands;

public class SetUserHasVehicleCommandHandler : IRequestHandler<SetUserHasVehicleCommand, SetUserHasVehicleCommandResponse>
{
    private readonly IMediator mediator;
    
    public SetUserHasVehicleCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task<SetUserHasVehicleCommandResponse> Handle(SetUserHasVehicleCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new BadRequestException("UserId is required.");
        var patchCommand = new PatchUpdateUserCommand
        {
            Id = request.UserId, // Use UserId as primary identifier
            HasVehicle = request.HasVehicle  // Only set this field
        };
        
        var result = await mediator.Send(patchCommand, cancellationToken);
        return new SetUserHasVehicleCommandResponse { Success = result.Success, Message = result.Message };
    }
}
