using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.GalleryModels;

namespace PetHotel.Areas.Client.Controllers
{
    [Area("Client")]
    public class ImageController : Controller
    {
        private readonly ICloudinaryImageUpload imageUpload;
        public ImageController(ICloudinaryImageUpload _imageUpload)
        {
            this.imageUpload = _imageUpload;
        }

        [HttpGet]
        [Route("UploadNewImage")]
        public IActionResult UploadNewImage()
        {
            var model = new UploadModel();

            return View(model);
        }

        [HttpPost]
        [Route("UploadNewImage")]
        public async Task<IActionResult> UploadNewImage(UploadModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await imageUpload.UploadPicture(model);
            return RedirectToAction("DisplayGallery", "Gallery");

        }
    }
}
