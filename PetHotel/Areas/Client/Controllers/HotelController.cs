using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;

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
            return View(await service.GetGustToAddAsync(Id));
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(AddGuestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await service.AddGuestAsync(model);

            return RedirectToAction("Profile","User");
        }
    }
}
