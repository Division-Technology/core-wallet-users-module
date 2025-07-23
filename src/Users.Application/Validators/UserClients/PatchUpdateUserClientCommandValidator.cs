// <copyright file="PatchUpdateUserClientCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Users.Domain.Entities.UserClients.Commands.PatchUpdate;

namespace Users.Application.Validators.UserClients;

public class PatchUpdateUserClientCommandValidator : AbstractValidator<PatchUpdateUserClientCommand>
{
    public PatchUpdateUserClientCommandValidator()
    {
        this.RuleFor(x => x.Id).NotEmpty();
        this.RuleFor(x => x.Language).MaximumLength(10).When(x => x.Language != null);
        this.RuleFor(x => x.Version).MaximumLength(50).When(x => x.Version != null);

        // Add more rules as needed for other fields
    }
}