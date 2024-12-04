using Bloggie.Webb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using System.Net;

namespace Bloggie.Webb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepositry imageRepositry;

        public ImagesController(IImageRepositry imageRepositry )
        {
            this.imageRepositry = imageRepositry;
        }
        [HttpPost]
        public async Task<IActionResult> UpLoadAsync(IFormFile file)
        {
            //call a repository
            var imageUrl = await imageRepositry.UploadAsync(file);

            if (imageUrl == null) 
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {Link = imageUrl});
        }


    }
}
