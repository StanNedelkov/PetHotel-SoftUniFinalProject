using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using PetHotel.Common;
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
        private readonly IUserService userService;
        public PetController(IPetService _service, IMemoryCache _cache, IUserService _userService)
        {
            this.service = _service;
            this.cache = _cache;
            this.userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Create")]
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");
            ViewData["PetTypeId"] = new SelectList(await GetTypes(), "Id", "Name");

            return View();
        }

        [Route("Create")]
        [HttpPost]

        public async Task<IActionResult> Create(CreatePetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["PetTypeId"] = new SelectList(await GetTypes(), "Id", "Name");
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
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            if (await userService.UserOwnsPet(userId, Id)) await service.DeletePetAsync(Id);

           
            return RedirectToAction("Profile", "User");
        }
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            if (!await userService.UserOwnsPet(userId, Id)) return RedirectToAction("Profile", "User"); 


            var model = await service.GetPetAsync(Id);
            ViewData["PetTypeId"] = new SelectList(await GetTypes(), "Id", "Name");
           return View(model);
            
            
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(CreatePetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["PetTypeId"] = new SelectList(await GetTypes(), "Id", "Name");
                return View(model); 
            }
            
                await service.EditPetAsync(model);
                return RedirectToAction("Profile", "User");
            
           
        }


        private async Task<IEnumerable<PetTypeViewModel>> GetTypes() 
            => await service.GetAllPetTypesAsync();
        
    }
}
