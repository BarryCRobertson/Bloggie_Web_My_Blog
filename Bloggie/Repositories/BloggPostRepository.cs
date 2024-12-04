using Bloggie.Webb.Data;
using Bloggie.Webb.Models.Domain;
using Bloggie.Webb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bloggie.Webb.Repositories
{
    public class BloggPostRepository : IBlogPostRepository
            {
        private readonly BloggieDbContext bloggieDbContext;
     

        public BloggPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
           
        }
       
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }
       

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.FindAsync(Id);
            if (existingBlog != null)
            {
                bloggieDbContext.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;    
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(X => X.Tags)
                .FirstOrDefaultAsync(X => X.UrlHandle == urlHandle);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog = await bloggieDbContext.BlogPosts.Include( x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id); 
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
