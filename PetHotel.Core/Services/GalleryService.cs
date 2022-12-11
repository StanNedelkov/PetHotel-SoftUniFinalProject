using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PetHotel.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class GalleryService : IGalleryService
    {
        private IWebHostEnvironment hostingEnv;
        public GalleryService(IWebHostEnvironment _hostingEnv)
        {
            this.hostingEnv = _hostingEnv;
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
