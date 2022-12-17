using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Common;
using PetHotel.Core.Contracts;

namespace PetHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    
    public class AdministratorController : Controller
    {
        private readonly IAdminService service;
        public AdministratorController(IAdminService _service)
        {
            this.service = _service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Profiles")]
        
        public async Task<IActionResult> Profiles()
        {
            var allUsers = await service.GetAllUsersAsync();

            return View(allUsers);
        }

        [Route("ActivateAccount")]
        public async Task<IActionResult>ActivateAccount(string Id)
        {
            if (!User.IsInRole(GlobalConstants.AdministratorRoleName)) return RedirectToAction("Index", "Home");

            await service.ActivateUserAsync(Id);

            return RedirectToAction(nameof(Profiles)); 
        }

        [Route("DeactivateAccount")]
        public async Task<IActionResult> DeactivateAccount(string Id)
        {
            if (!User.IsInRole(GlobalConstants.AdministratorRoleName)) return RedirectToAction("Index", "Home");

            await service.DeactivateUserAsync(Id);

            return RedirectToAction(nameof(Profiles));
        }
    }
}
