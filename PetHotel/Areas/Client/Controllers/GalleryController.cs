using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.GalleryModels;

namespace PetHotel.Areas.Client.Controllers
{
    [Authorize]
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

        /*  [Route("UploadFiles")]
          [HttpPost]
          public IActionResult UploadFiles()
          {
              long size = 0;
              var files = Request.Form.Files;

              foreach (var file in files)
              {
                  string filename = hostingEnv.WebRootPath + $@"\uploadedfiles\{file.FileName}";
                  size += file.Length;
                  using (FileStream fs = System.IO.File.Create(filename))
                  {
                      file.CopyTo(fs);
                      fs.Flush();
                  }
              }
              string message = $"{files.Count} file(s) / {size}bytes uploaded successfully!";
              return Json(message);
          }*/

        [Route("Upload")]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            
            await service.UploadFileAsync(File);

            return RedirectToAction(nameof(Index));
        }
    }
}
