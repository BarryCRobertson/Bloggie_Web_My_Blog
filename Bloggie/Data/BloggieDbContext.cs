using Bloggie.Webb.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Bloggie.Webb.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
            

        }   
        public DbSet<BlogPost> BlogPosts { get; set; }
        
        public  DbSet<Tag> Taggs { get; set; }
        
        public DbSet<BlogPostLike> BlogPostLike { get; set; }

        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}
