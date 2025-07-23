// <copyright file="UserClientConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Data.Tables;
using Users.Domain.Enums;

namespace Users.Data.Configurations.UserClients;

public class UserClientConfiguration : IEntityTypeConfiguration<UserClient>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UserClient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ChannelType).IsRequired().HasConversion<int>();
        builder.Property(x => x.TelegramId).HasMaxLength(64);
        builder.Property(x => x.ChatId).HasMaxLength(64);
        builder.Property(x => x.DeviceToken).HasMaxLength(256);
        builder.Property(x => x.SessionId).HasMaxLength(128);
        builder.Property(x => x.Platform).HasMaxLength(32);
        builder.Property(x => x.Version).HasMaxLength(32);
        builder.Property(x => x.Language).HasMaxLength(10);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.HasIndex(x => x.UserId).HasDatabaseName("idx_clients_user_id");
        builder.HasIndex(x => x.ChannelType).HasDatabaseName("idx_clients_channel_type");
        builder.HasIndex(x => x.TelegramId).HasDatabaseName("idx_clients_telegram_id");
        builder.HasIndex(x => x.DeviceToken).HasDatabaseName("idx_clients_device_token");
        builder.HasIndex(x => x.SessionId).HasDatabaseName("idx_clients_session_id");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}