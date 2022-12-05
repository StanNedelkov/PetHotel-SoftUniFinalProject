using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using System.Security.Claims;

namespace PetHotel.Areas.Client.Controllers
{
    [Authorize]
    [Area("Client")]
    public class HotelController : Controller
    {
        private readonly IHotelService service;
        public HotelController(IHotelService _service)
        {
            this.service = _service;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("Add")]
        [HttpGet]
        public async Task<IActionResult>Add(int Id)
        {
            return View(await service.GetGuestToAddAsync(Id));
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(AddGuestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await service.AddGuestAsync(model);

                return RedirectToAction("Profile", "User");
            }
            catch (ArgumentException)
            {
                return View(model);
            }
            
        }

        [Route("AllMine")]
        /*[HttpPost]*/
        public async Task<IActionResult> AllMine()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var allMyPetsInHotel = await service.GetMyAllGuestsAsync(userId);
            return View(allMyPetsInHotel);
        }

        //todo edit the basicpetviewmodel to have the schedule Id in order to use it here

        [Route("Cancel")]
        public async Task <IActionResult> Cancel(int id)
        {
            await service.CancelHotelStayAsync(id);

            return RedirectToAction(nameof(AllMine));
        }
    }
}
