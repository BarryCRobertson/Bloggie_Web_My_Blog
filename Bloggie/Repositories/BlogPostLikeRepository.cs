﻿using Bloggie.Webb.Data;
using Bloggie.Webb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Webb.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike.
                  CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike.
                CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}