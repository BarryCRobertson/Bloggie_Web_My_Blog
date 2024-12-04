using Bloggie.Webb.Models.Domain;

namespace Bloggie.Webb.Repositories
{
    public interface ITagRepository
    {
     
        Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery=null,
            string? sortBy=null,
            string? sortDirection=null,
            int pageNumber = 1,
            int pageSize = 3 );

        Task<Tag?> GetAsync(Guid Id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid Id);

        Task<int> CountAsync();

    }
}
