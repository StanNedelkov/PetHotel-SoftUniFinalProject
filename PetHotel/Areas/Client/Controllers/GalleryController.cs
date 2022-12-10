using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace PetHotel.Areas.Client.Controllers
{
    [Authorize]
    [Area("Client")]
    public class GalleryController : Controller
    {
        private IWebHostEnvironment hostingEnv;
        public GalleryController(IWebHostEnvironment _hostingEnv)
        {
            this.hostingEnv = _hostingEnv;
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
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string fileDic = 
                "UploadedFiles";

            string filePath = 
                Path.Combine(hostingEnv.WebRootPath, fileDic);

            if (!Directory.Exists(filePath)) 
                Directory.CreateDirectory(filePath);

            string fileName = 
                file.FileName;

            filePath= 
                Path.Combine(filePath, fileName);

            using (FileStream stream = System.IO.File.Create(filePath))
                await file.CopyToAsync(stream);

                return RedirectToAction(nameof(Index));
        }
    }
}
