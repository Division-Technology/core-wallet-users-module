// <copyright file="CreateUserClientCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Users.Domain.Entities.UserClients.Commands.Create;

namespace Users.Application.Validators.UserClients;

public class CreateUserClientCommandValidator : AbstractValidator<CreateUserClientCommand>
{
    public CreateUserClientCommandValidator()
    {
        this.RuleFor(x => x.UserId).NotEmpty();
        this.RuleFor(x => x.Type).IsInEnum();
        this.RuleFor(x => x.ClientData).NotNull();

        // Add more rules as needed for client data validation
    }
}