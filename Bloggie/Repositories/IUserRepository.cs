using Microsoft.AspNetCore.Identity;

namespace Bloggie.Webb.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll(); 
        

    }
}
