namespace Bloggie.Webb.Repositories
{
    public interface IImageRepositry
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
