using Bloggie.Webb.Data;
using Bloggie.Webb.Models.Domain;
using Bloggie.Webb.Models.ViewModels;
using Bloggie.Webb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bloggie.Webb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository) 
        {
            this.tagRepository = tagRepository;
        }
       
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            validateAddtagRequest(addTagRequest);
            if(ModelState.IsValid == false)
            {
                return View();
            }
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
                
               await tagRepository.AddAsync(tag);
                return RedirectToAction("List");
            
            //return View("Add");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageSize = 3,
            int pageNumber = 1)
        {
           
            var totalRecords = await tagRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }
            if (pageNumber < 1)
            {
                pageNumber++;
            }



            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            var tags = await tagRepository.GetAllAsync(searchQuery, sortBy, sortDirection,pageNumber,pageSize);
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                    
                };
                return View(editTagRequest);
            }


            return View(null);
        }
        public async Task<IActionResult> Edit(EditTagRequest  editTagRequest)
        {
            var tag = new Tag
            { 
              Id=editTagRequest.Id,
              Name = editTagRequest.Name,
              DisplayName = editTagRequest.DisplayName
            
            };
       var updateTag = await tagRepository.UpdateAsync(tag);
            if (updateTag != null)
            {
            
            }
            else
            {
            
            }
             
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
             
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }
           
           
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        private void validateAddtagRequest(AddTagRequest request)
        {
            if(request.Name is not null && request.DisplayName is not null)
            {
                if(request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be he same as DisplayName");
                }
            }
        }
    }
}
