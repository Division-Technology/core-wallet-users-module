// <copyright file="PatchUpdateUserCommandValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Users.Domain.Entities.Users.Commands.PatchUpdate;

namespace Users.Application.Validators.Users;

public class PatchUpdateUserCommandValidator : AbstractValidator<PatchUpdateUserCommand>
{
    public PatchUpdateUserCommandValidator()
    {
        this.RuleFor(x => x.Id).NotEmpty();
        this.RuleFor(x => x.FirstName).MaximumLength(100).When(x => x.FirstName != null);
        this.RuleFor(x => x.LastName).MaximumLength(100).When(x => x.LastName != null);
        this.RuleFor(x => x.PhoneNumber).Matches(@"^\+?[1-9]\d{1,14}$").When(x => x.PhoneNumber != null);
        this.RuleFor(x => x.Language).MaximumLength(10).When(x => x.Language != null);

        // Add more rules as needed for other fields
    }
}
