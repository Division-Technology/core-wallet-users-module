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
        this.RuleFor(x => x.ChannelType).IsInEnum();
        // Add rules for required delivery/metadata fields based on ChannelType
        When(x => x.ChannelType == ChannelType.TelegramBot, () =>
        {
            this.RuleFor(x => x.TelegramId).NotEmpty();
            this.RuleFor(x => x.ChatId).NotEmpty();
        });
        When(x => x.ChannelType == ChannelType.MobileApp, () =>
        {
            this.RuleFor(x => x.DeviceToken).NotEmpty();
        });
        When(x => x.ChannelType == ChannelType.WebApp, () =>
        {
            this.RuleFor(x => x.SessionId).NotEmpty();
        });
    }
}