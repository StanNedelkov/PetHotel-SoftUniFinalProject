using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;

namespace PetHotel.Areas.Client.Controllers
{
    [AllowAnonymous]
    [Area("Client")]
    public class GalleryController : Controller
    {
        private IWebHostEnvironment hostingEnv;
        private readonly IGalleryService service;
        public GalleryController(IWebHostEnvironment _hostingEnv, IGalleryService _service)
        {
            this.hostingEnv = _hostingEnv;
            this.service = _service;
        }
        [Route("Index")]
        
        public IActionResult Index()
        {
            return View();
        }


        [Route("DisplayGallery")]
       
        public IActionResult DisplayGallery()
        {
            var imageList = service.GetAll();

            var model = service.GetIndexModel(imageList);

            return View(model);
        }
        [Route("Details")]
        
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(DisplayGallery));

            var image = await service.GetById(id ?? 0);

            if (image == null) return RedirectToAction(nameof(DisplayGallery));

            var model = service.GetDetailsModel(image);

            return View(model);
        }
    }
}
