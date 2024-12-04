using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Webb.Repositories
{
    public class CloudinaryImageRepository : IImageRepositry
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
             configuration.GetSection("Cloudinary")["CloudName"],
             configuration.GetSection("Cloudinary")["ApiKey"],
             configuration.GetSection("Cloudinary")["ApiSecret"]
             );

        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,

            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if (uploadResult != null)// = System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
