using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using System.Security.Claims;

namespace PetHotel.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    
    public class HotelController : Controller
    {

        private readonly IHotelService service;
        private readonly IUserService userService;
        private readonly IEmployeeService employeeService;
        public HotelController(IHotelService _service, IUserService _userService, IEmployeeService _employeeService)
        {
            this.service = _service;
            this.userService = _userService;
            this.employeeService = _employeeService;
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
                var allMyPetsInHotel = await service.GetAllGuestsTodayAsync();
                return View(allMyPetsInHotel);
            }
            catch (Exception)
            {
                //todo something
                throw;
            }

        }
        [Route("CheckIn")]
        [HttpGet]
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("CheckIn")]
        [HttpPost]
        public async Task<IActionResult> CheckIn(GuestDetailedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.ManageStatusAsync(model.ReservationId, GlobalConstants.InProgressStatus);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Today");
        }


        [Route("Cancel")]
        [HttpGet]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("Cancel")]
        [HttpPost]
        public async Task<IActionResult> Cancel(GuestDetailedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.ManageStatusAsync(model.ReservationId, GlobalConstants.CanceledStatus);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Today");
        }
        private IEnumerable<string> Statuses() 
            => employeeService.GetStatusList();

        [Route("Depart")]
        /*[HttpPost]*/
        public async Task<IActionResult> Depart()
        {
            /* if (!User.IsInRole(GlobalConstants.EmployeeRoleName))
             {
                 //not authorized redirect
                 return RedirectToAction("","");
             }*/
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            try
            {
                var theDeparted = await employeeService.GetDeparturesTodayAsync();
                return View(theDeparted);
            }
            catch (Exception)
            {
                //todo something
                throw;
            }
        }


        [Route("CheckOut")]
        [HttpGet]
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("CheckOut")]
        [HttpPost]
        public async Task<IActionResult> CheckOut(GuestDetailedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.ManageStatusAsync(model.ReservationId, GlobalConstants.CompletedStatus);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Today");
        }
    }
}
