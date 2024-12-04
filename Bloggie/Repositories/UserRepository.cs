using Bloggie.Webb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Webb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }


        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var SuperAdminUser = await authDbContext.Users.
                FirstOrDefaultAsync(x => x.Email == "SuperAdminUser@Bloggie.com");
            if (SuperAdminUser != null) 
            { 
                users.Remove(SuperAdminUser);
            }
            return users;
        }
    }
}
