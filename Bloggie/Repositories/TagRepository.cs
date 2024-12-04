using Bloggie.Webb.Data;
using Bloggie.Webb.Models.Domain;
using Bloggie.Webb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Webb.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Taggs.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<int> CountAsync()
        {
            return await bloggieDbContext.Taggs.CountAsync();   
        }

        public async Task<Tag?> DeleteAsync(Guid Id)
        {
            var exsistingTag = await bloggieDbContext.Taggs.FindAsync(Id);
            if (exsistingTag != null)
            {
                bloggieDbContext.Taggs.Remove(exsistingTag);
                await bloggieDbContext.SaveChangesAsync();

                return exsistingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageNumber= 1,
            int pageSize= 3)

        {
            
            var query = bloggieDbContext.Taggs.AsQueryable();
            //Filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) ||
                                        x.DisplayName.Contains(searchQuery));

            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);
                {
                    if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                    {
                        query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    }
                    if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                    }
                }

            }
            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

                return await query.ToListAsync();


            //return await bloggieDbContext.Taggs.ToListAsync();



        }
        public async Task<Tag?> GetAsync(Guid Id)
        {
            return await bloggieDbContext.Taggs.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Taggs.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}       

       
    
