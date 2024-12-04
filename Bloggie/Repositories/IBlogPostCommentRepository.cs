using Bloggie.Webb.Models.Domain;

namespace Bloggie.Webb.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid BlogPostId);
    }
}
