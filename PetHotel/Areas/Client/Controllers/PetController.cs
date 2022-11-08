using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
using System.Security.Claims;

namespace PetHotel.Areas.Client.Controllers
{
    [Authorize]
    [Area("Client")]
    public class PetController : Controller
    {
        private readonly IPetService service;
        private readonly IMemoryCache cache;
        public PetController(IPetService _service, IMemoryCache _cache)
        {
            this.service = _service;
            this.cache = _cache;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Create")]
        [HttpGet]

        public async Task<IActionResult> Create()
        {

            ViewData["PetTypeId"] = new SelectList(await service.GetAllPetTypesAsync(), "Id", "Name");

           /* cache.Set("PetTypeId", types);*/
            return View();
        }

        [Route("Create")]
        [HttpPost]

        public async Task<IActionResult> Create(CreatePetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["PetTypeId"] = new SelectList(await service.GetAllPetTypesAsync(), "Id", "Name");
                return View(model);
            }

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            await service.AddPetAsync(model, userId);
            return RedirectToAction("Profile", "User");
        }

        [Route("Delete")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult>Delete(int Id)
        {
            try
            {
                await service.DeletePetAsync(Id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return RedirectToAction("Profile", "User");
        }

    }
}
