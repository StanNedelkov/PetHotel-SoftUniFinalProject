using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.Core.Services
{
    public class GalleryService : IGalleryService
    {
        private IWebHostEnvironment hostingEnv;

        private readonly PetHotelDbContext context;
        public GalleryService(IWebHostEnvironment _hostingEnv, PetHotelDbContext _context)
        {
            this.hostingEnv = _hostingEnv;
            this.context = _context;
        }

        public IEnumerable<GalleryImage> GetAll()
        {
            return context.GalleryImages.Include(x => x.Tags);
        }

        //to do null check and async
        public async Task<GalleryImage> GetById(int id)
        {
            return await context
                .GalleryImages
                .FindAsync(id);

            
        }
        //saves files to folder, not implemented, old version, TO BE REMOVED
        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return  GetAll()
                .Where(img => img.Tags.Any(t => t.Description == tag));
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            string fileDic =
                "UploadedFiles";

            string filePath =
                Path.Combine(hostingEnv.WebRootPath, fileDic);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fileName =
                file.FileName;

            filePath =
                Path.Combine(filePath, fileName);

            using (FileStream stream = System.IO.File.Create(filePath))
                await file.CopyToAsync(stream);

        }
    }
}
