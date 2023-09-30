using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersModule.Data.Tables;

namespace UsersModule.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(user => user.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(user => user.LastName).HasColumnName("last_name").IsRequired();
    }
}