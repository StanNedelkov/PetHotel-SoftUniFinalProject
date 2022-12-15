using Microsoft.AspNetCore.Http;
using PetHotel.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.GalleryModels
{
    public class UploadModel
    {
        [AllowedExtentions(new string[] { ".gif", ".jpeg", ".png" })]
        public IFormFile File { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Tags { get; set; } = null!;
    }
}
