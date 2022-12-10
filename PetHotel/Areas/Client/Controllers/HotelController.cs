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

        [Route("Add")]
        [HttpGet]
        public async Task<IActionResult> Add(int Id)
        {
            //check if pet is owned by logged in user.
            string loggedUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            if (!await userService.UserOwnsPet(loggedUserId, Id)) return RedirectToAction(nameof(AllMine));

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
            try
            {
                var allMyPetsInHotel = await service.GetMyAllGuestsAsync(userId);
                return View(allMyPetsInHotel);
            }
            catch (Exception)
            {
                //todo something
                throw;
            }

        }


        [Route("Cancel")]

        public async Task<IActionResult> Cancel(int id) //this is the id of the reservation
        {
            //check if pet is owned by logged in user.
            

            try
            {
                await service.CancelHotelStayAsync(id);
                return RedirectToAction(nameof(AllMine));
            }
            catch (Exception)
            {
                //todo something
                throw;
            }
        }

        [Route("EditReservation")]
        [HttpGet]
        public async Task<IActionResult> Edit(int Id) //this is the id of the pet

        {   //check if pet is owned by logged in user.
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            if (!await userService.UserOwnsPet(userId, Id)) return RedirectToAction(nameof(AllMine));
            return View(await service.GetGuestToEditAsync(Id));
        }

        [Route("EditReservation")]
        [HttpPost]
        public async Task<IActionResult> Edit(AddGuestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await service.EditGuestAsync(model);

                return RedirectToAction("Profile", "User");
            }
            catch (ArgumentException)
            {
                return View(model);
            }

        }
    }
}
