// <copyright file="SetUserLanguageCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CS8601 // False positive - we've already validated that Language is not null

using MediatR;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Commands.SetLanguage;
using Users.Domain.Entities.Users.Commands.PatchUpdate;

namespace Users.Application.Handlers.Users.Commands;

public class SetUserLanguageCommandHandler : IRequestHandler<SetUserLanguageCommand, SetUserLanguageCommandResponse>
{
    private readonly IMediator mediator;
    
    public SetUserLanguageCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task<SetUserLanguageCommandResponse> Handle(SetUserLanguageCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new BadRequestException("UserId is required.");
        if (string.IsNullOrWhiteSpace(request.Language))
        {
            throw new BadRequestException("Language is required.");
        }

        var language = request.Language!; // This is guaranteed to be non-null after validation

        var patchCommand = new PatchUpdateUserCommand
        {
            Id = request.UserId, // Use UserId as primary identifier
            Language = language  // Only set this field
        };
        
        var result = await mediator.Send(patchCommand, cancellationToken);
        return new SetUserLanguageCommandResponse { Success = result.Success, Message = result.Message };
    }
}