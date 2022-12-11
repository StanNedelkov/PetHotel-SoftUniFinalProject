using Microsoft.AspNetCore.Http;
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
       
    }
}
