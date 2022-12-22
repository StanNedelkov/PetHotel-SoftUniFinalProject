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

        [Route("ControlMenu")]
        public IActionResult ControlMenu()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName))  return RedirectToAction("Index", "Home");
            
            var model = employeeService.Counter();
            return View(model);
        }



        [Route("Overdue")]
        /*[HttpPost]*/
        public async Task<IActionResult> Overdue()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");

          
            
                var allMyPetsInHotel = await employeeService.GetOverdueAsync();
                return View(allMyPetsInHotel);

        }

        [Route("AllInHotel")]
        /*[HttpPost]*/
        public async Task<IActionResult> AllInHotel()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");
            
                var allMyPetsInHotel = await employeeService.GetAllInHotelAsync();
                return View(allMyPetsInHotel);
           

        }

        [Route("All")]
        /*[HttpPost]*/
        public async Task<IActionResult> All()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");
            
                var allMyPetsInHotel = await employeeService.GetAllAsync();
                return View(allMyPetsInHotel);
            

        }

        [Route("Today")]
        /*[HttpPost]*/
        public async Task<IActionResult> Today()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");

            var allMyPetsInHotel = await service.GetAllGuestsTodayAsync();
                return View(allMyPetsInHotel);
            

        }
        [Route("CheckIn")]
        [HttpGet]
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return NotFound();
            }
            
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
            
        }

        [Route("CheckIn")]
        [HttpPost]
        public async Task<IActionResult> CheckIn(GuestDetailedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
             await employeeService.ManageStatusAsync(model.ReservationId, GlobalConstants.InProgressStatus);
            
            return RedirectToAction("Today");
        }


        [Route("Cancel")]
        [HttpGet]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }
            
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
           
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
       

        [Route("Depart")]
        /*[HttpPost]*/
        public async Task<IActionResult> Depart()
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");

            var theDeparted = await employeeService.GetDeparturesTodayAsync();
                return View(theDeparted);
            
           
        }


        [Route("CheckOut")]
        [HttpGet]
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (!User.IsInRole(GlobalConstants.EmployeeRoleName)) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return NotFound();
            }
            
                var model = await employeeService.GetReservationAsync(id ?? 0);

                return View(model);
            
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
