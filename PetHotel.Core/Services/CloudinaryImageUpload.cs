using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.GalleryModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.Core.Services
{
    public class CloudinaryImageUpload : ICloudinaryImageUpload
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly PetHotelDbContext context;


        public CloudinaryImageUpload(
            IConfiguration _configuration, 
            IWebHostEnvironment _webHostEnvironment,
            PetHotelDbContext _context)
        {
            this.ApiKey = _configuration["Cloudinary:ApiKey"];
            this.ApiSecret = _configuration["Cloudinary:ApiSecret"];
            this.Cloud = _configuration["Cloudinary:Cloud"];
            this.Account = new Account { ApiKey = this.ApiKey, ApiSecret = this.ApiSecret, Cloud = this.Cloud };
            this.configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            context = _context;
        }


        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private string Cloud { get; set; }
        private Account Account { get; set; }
        public async Task<string> UploadPicture(UploadModel model)
        {
            var cloudinary = new Cloudinary(Account);
            cloudinary.Api.Secure = true;



            //reads the Image in the IFormFile into a string
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                model.File.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);

            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64;
           
            //File and path for Cloudinary upload

            var uploadParams = new ImageUploadParams()

            {
                File = new FileDescription(imagePath),
                Folder = "EndSars/img"
            };

            //the url of the uploaded picture
            var uploadResult = await cloudinary.UploadAsync(@uploadParams);
            


            // adds the new image to be uploaded to the databse
            var image = new GalleryImage()
            {
                Title = model.Title,
                Created = DateTime.Now,
                Url = uploadResult.Url.AbsoluteUri,
                Tags = ParseTags(model.Tags),
                HotelID = GlobalConstants.CatsDogsAndCrocsHotelId
            };
            await context.AddAsync(image);
            await context.SaveChangesAsync();

           // return OkResult;
            return uploadResult.SecureUrl.AbsoluteUri;
        }




        public List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new ImageTag
            {

                Description = tag

            }).ToList();

        }
    }
}
