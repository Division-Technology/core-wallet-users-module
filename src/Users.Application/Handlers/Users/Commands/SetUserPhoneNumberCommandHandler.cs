// <copyright file="SetUserPhoneNumberCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS8601 // False positive - we've already validated that NewPhoneNumber is not null

using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Commands.SetPhoneNumber;
using Users.Domain.Entities.Users.Commands.PatchUpdate;

namespace Users.Application.Handlers.Users.Commands;

public class SetUserPhoneNumberCommandHandler : IRequestHandler<SetUserPhoneNumberCommand, SetUserPhoneNumberCommandResponse>
{
    private readonly IMediator mediator;
    
    public SetUserPhoneNumberCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task<SetUserPhoneNumberCommandResponse> Handle(SetUserPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new BadRequestException("UserId is required.");
        if (string.IsNullOrWhiteSpace(request.NewPhoneNumber))
        {
            throw new BadRequestException("New phone number is required.");
        }

        var phoneNumber = request.NewPhoneNumber!; // This is guaranteed to be non-null after validation

        var patchCommand = new PatchUpdateUserCommand
        {
            Id = request.UserId, // Use UserId as primary identifier
            PhoneNumber = phoneNumber  // Only set this field
        };
        
        var result = await mediator.Send(patchCommand, cancellationToken);
        return new SetUserPhoneNumberCommandResponse { Success = result.Success, Message = result.Message };
    }
}