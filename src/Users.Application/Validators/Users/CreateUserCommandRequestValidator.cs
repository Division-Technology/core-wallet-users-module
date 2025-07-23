// <copyright file="CreateUserCommandRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Users.Domain.Entities.Users.Commands.Create;

namespace Users.Application.Validators.Users;

public class CreateUserCommandRequestValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandRequestValidator()
    {
        this.RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$");
        this.RuleFor(x => x.Language).MaximumLength(10).When(x => x.Language != null);

        this.RuleFor(x => x.TelegramId).GreaterThan(0).When(x => x.TelegramId.HasValue);
        this.RuleFor(x => x.ChatId).GreaterThan(0).When(x => x.ChatId.HasValue);
        this.RuleFor(x => x.Username).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Username));

        // Add more rules as needed
    }
}
