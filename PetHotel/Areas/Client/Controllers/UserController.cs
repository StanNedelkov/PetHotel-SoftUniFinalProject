using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data.Entities;
using System.Security.Claims;

namespace PetHotel.Areas.Client.Controllers
{

    /// <summary>
    /// UserController handles login, register and logout functions by calling UserService.
    /// </summary>
    [Authorize]
    [Area("Client")]
    public class UserController : Controller
    {
        private readonly IUserService service;
        private readonly UserManager<User> userManager;
        public UserController(IUserService _service, UserManager<User> _userManager)
        {
            service = _service;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await service.RegisterNewUserAsync(model);
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError(string.Empty, ae.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await service.LoginUserAsync(model);
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError(string.Empty, ae.Message);
                return View(model);
            }
            var user = await service.EmployeeUser();
            if (await userManager.IsInRoleAsync(user, "Employee")) return Redirect("Employee/Hotel/Index");
            
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logout. Doesn't need to check if the user is logged in
        /// because of the [Authorize] attribute for this controller.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await service.SignOutUserAsync();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// View the logged user's profile information and pets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            try
            {
                var userProfile = await service.GetProfileAsync(userId!); 
                return View(userProfile);
            }
            catch (ArgumentNullException ne)
            {
                //TODO error page redirect
                return BadRequest(ne.Message);
            }
        }

        [HttpGet]
        [Route("MyPets")]

        public async Task<IActionResult> MyPets()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            try
            {
                var pets = await service.GetMyPetsAsync(userId);
                return View(pets);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
          
    }
}
