using Bloggie.Webb.Data;
using Bloggie.Webb.Models.Domain;
using Bloggie.Webb.Models.ViewModels;
using Bloggie.Webb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.Webb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
            //BlogPostRepository = blogPostRepository;
        }

        public IBlogPostRepository blogPostRepository { get; }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBloggPostRequest
            {
                Tag = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })

            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBloggPostRequest addBloggPostRequest)
        {
            // May view model to Domain Model
            var blogpost = new BlogPost
            {
                Heading = addBloggPostRequest.Heading,
                PageTitle = addBloggPostRequest.PageTitle,
                Content = addBloggPostRequest.Content,
                ShortDescription = addBloggPostRequest.ShortDescription,
                FeaturedImageUrl = addBloggPostRequest.FeaturedImageUrl,
                UrlHandle = addBloggPostRequest.UrlHandle,
                PublishedDate = addBloggPostRequest.PublishedDate,
                Author = addBloggPostRequest.Author,
                Visible = addBloggPostRequest.Visible,
            };
            // Map tags from Selected Tags
            var selectedTags = new List<Tag>();
            foreach (var selectedtagId in addBloggPostRequest.SelectedTags)
            {
                var selectedTagIdASGuid = Guid.Parse(selectedtagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdASGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
                // Mapping tags back to Domain Model

            }
            blogpost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogpost);

            return RedirectToAction("Add");

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, string[] selectedTags)
        {
            // Rertrieve the result from the repository
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();
            //Map the domain model into the view model.
            if (blogPost != null)
            {
                var model = new EditBloggPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()

                };


                return View(model);      // Pass data to blogView


            }

            return View(null);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBloggPostRequest editBloggPostRequest)
        {
            // Map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBloggPostRequest.Id,
                Heading = editBloggPostRequest.Heading,
                PageTitle = editBloggPostRequest.PageTitle,
                Content = editBloggPostRequest.Content,
                Author = editBloggPostRequest.Author,
                ShortDescription = editBloggPostRequest.ShortDescription,
                FeaturedImageUrl = editBloggPostRequest.FeaturedImageUrl,
                PublishedDate = editBloggPostRequest.PublishedDate,
                UrlHandle = editBloggPostRequest.UrlHandle,
                Visible = editBloggPostRequest.Visible,
            };
            // Map Tags into domain model
            var SelectedTags = new List<Tag>();
            foreach (var selectedTag in editBloggPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);
                    if (foundTag != null)
                    {
                        SelectedTags.Add(foundTag);
                    }

                }

            }
            blogPostDomainModel.Tags = SelectedTags;
            //Submit info to repository to update
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                /// Show success
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBloggPostRequest editBloggPostRequest)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBloggPostRequest.Id);
            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editBloggPostRequest.Id });
              


        }
    }
}
