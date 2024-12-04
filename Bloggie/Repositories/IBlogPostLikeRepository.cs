using Bloggie.Webb.Models.Domain;

namespace Bloggie.Webb.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid BlogPost);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

        Task<int> GetLikesForBlog(Guid BlogPostId);
    }
}
