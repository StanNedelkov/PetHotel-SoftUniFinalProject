using Microsoft.AspNetCore.Http;
using PetHotel.Common;
using PetHotel.Common.EntityConstants;
using PetHotel.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Core.Models.GalleryModels
{
    public class UploadModel
    {
        [Required]
        [AllowedExtentions(new string[] { ".gif", ".jpg", ".png" }, ErrorMessage = ErrorMessagesConstants.fileExtentionInvalid)]
        public IFormFile File { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Tags { get; set; } = null!;
    }
}
