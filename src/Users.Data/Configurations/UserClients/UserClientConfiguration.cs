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
        builder.Property(x => x.Type).IsRequired().HasConversion<int>();
        builder.Property(x => x.ClientData).HasColumnType("jsonb"); // PostgreSQL JSONB for efficient querying
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.HasIndex(x => x.UserId).HasDatabaseName("idx_clients_user_id");
        builder.HasIndex(x => x.Type).HasDatabaseName("idx_clients_type");
        builder.HasIndex(x => new { x.Type, x.ClientData }).HasDatabaseName("idx_clients_type_data"); // For querying specific client data
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}