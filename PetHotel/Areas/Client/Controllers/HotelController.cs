using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Common;
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
        public async Task<IActionResult> Add(int Id)//this is the id of the pet
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");

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
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value; 
            
                var allMyPetsInHotel = await service.GetMyAllGuestsAsync(userId);
                return View(allMyPetsInHotel);
            

        }


        [Route("CancelReserv")]

        public async Task<IActionResult> CancelReserv(int? id) //this is the id of the reservation
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            //here the user is checked in the service
            await service.CancelHotelStayAsync(id ?? 0, userId);
               return RedirectToAction(nameof(AllMine));
            
        }

        [Route("EditReservation")]
        [HttpGet]
        public async Task<IActionResult> EditReservation(int Id) //this is the id of the pet
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName)) return RedirectToAction("Index", "Home");

            //check if pet is owned by logged in user.
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            if (!await userService.UserOwnsPet(userId, Id)) return RedirectToAction(nameof(AllMine));

            return View(await service.GetGuestToEditAsync(Id));
        }

        [Route("EditReservation")]
        [HttpPost]
        public async Task<IActionResult> EditReservation(AddGuestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            
                await service.EditGuestAsync(model);

                return RedirectToAction("Profile", "User");
            

        }
    }
}
