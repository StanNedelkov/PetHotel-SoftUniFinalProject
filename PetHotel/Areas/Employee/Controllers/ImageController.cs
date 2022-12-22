using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.GalleryModels;

namespace PetHotel.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ICloudinaryImageUpload imageUpload;
        public ImageController(ICloudinaryImageUpload _imageUpload)
        {
            imageUpload = _imageUpload;
        }

        [HttpGet]
        [Route("UploadNewImage")]
        public IActionResult UploadNewImage()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");

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
            return RedirectToAction("DisplayGallery", "Gallery", new { area = "Client" });

        }
    }
}
