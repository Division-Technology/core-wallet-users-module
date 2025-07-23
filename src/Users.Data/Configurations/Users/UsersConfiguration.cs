// <copyright file="UsersConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Data.Tables;
using Users.Domain.Enums;

namespace Users.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Language).HasMaxLength(10);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.RegistrationStatus).HasConversion<int>().HasDefaultValue(RegistrationStatus.Unregistered);
        builder.Property(x => x.IsBlock).HasDefaultValue(false);
        builder.Property(x => x.IsAdmin).HasDefaultValue(false);
        builder.Property(x => x.IsSuspicious).HasDefaultValue(false);
        builder.Property(x => x.IsPremium).HasDefaultValue(false);
        builder.Property(x => x.HasVehicle).HasDefaultValue(false);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(x => x.ModifiedAt).HasDefaultValueSql("now()");
        builder.HasIndex(x => x.PhoneNumber).HasDatabaseName("idx_users_phone_number");
        builder.HasIndex(x => x.Referrer).HasDatabaseName("idx_users_referrer");
        builder.HasIndex(x => x.RegistrationStatus).HasDatabaseName("idx_users_registration_status");
        builder.HasIndex(x => x.IsBlock).HasDatabaseName("idx_users_block");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.Referrer)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
