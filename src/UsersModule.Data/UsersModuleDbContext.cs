using Microsoft.EntityFrameworkCore;
using UsersModule.Data.Configurations.Users;
using UsersModule.Data.Tables;

namespace UsersModule.Data;

public class UsersModuleDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersModuleDbContext() { }
    
    public UsersModuleDbContext(DbContextOptions<UsersModuleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UsersConfiguration().Configure(modelBuilder.Entity<User>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO: use Dependency Injection instead of hardcoded connection string
        optionsBuilder.UseNpgsql("Host=localhost;Port=9000;Database=users_module_db;Username=postgres;Password=postgres");
    }
}