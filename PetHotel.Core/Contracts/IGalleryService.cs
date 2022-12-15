using Microsoft.AspNetCore.Http;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface IGalleryService
    {
        Task UploadFileAsync(IFormFile file);

        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        Task<GalleryImage> GetById(int id);

    }
}
