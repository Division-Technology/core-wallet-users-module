using Microsoft.EntityFrameworkCore;
using UsersModule.Data;
using UsersModule.Data.Tables;
using UsersModule.Repositories.Common;

namespace UsersModule.Repositories.Users;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(UsersModuleDbContext dbContext) : base(dbContext)
    {
    }
}