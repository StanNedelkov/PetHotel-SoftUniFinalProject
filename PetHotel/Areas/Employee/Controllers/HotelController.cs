using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using System.Security.Claims;

namespace PetHotel.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    
    public class HotelController : Controller
    {

        private readonly IHotelService service;
        private readonly IUserService userService;

        public HotelController(IHotelService _service, IUserService _userService)
        {
            this.service = _service;
            this.userService = _userService;

        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("Today")]
        /*[HttpPost]*/
        public async Task<IActionResult> Today()
        {
           /* if (!User.IsInRole(GlobalConstants.EmployeeRoleName))
            {
                //not authorized redirect
                return RedirectToAction("","");
            }*/
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            try
            {
                var allMyPetsInHotel = await service.GetAllGuestsAsync();
                return View(allMyPetsInHotel);
            }
            catch (Exception)
            {
                //todo something
                throw;
            }

        }
    }
}
