using Bloggie.Webb.Models.Domain;

namespace Bloggie.Webb.Repositories
{
    public interface IBlogPostRepository
    {
        Task <IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync (Guid Id);

        Task<BlogPost?> GetUrlHandleAsync(string urlHandle);

        Task<BlogPost> AddAsync (BlogPost blogPost);

        Task<BlogPost?> UpdateAsync (BlogPost blogPost);

        Task<BlogPost?> DeleteAsync (Guid Id);

    }
}
