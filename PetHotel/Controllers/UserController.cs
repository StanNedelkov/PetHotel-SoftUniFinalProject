using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.UserModels;
using System.Security.Claims;

namespace PetHotel.Controllers
{
    /// <summary>
    /// UserController handles login, register and logout functions by calling UserService.
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService service;
        public UserController(IUserService _service)
        {
            service = _service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View( model);
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>Login(LoginViewModel model)
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
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logout. Doesn't need to check if the user is logged in
        /// because of the [Authorize] attribute for this controller.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
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
        public async Task<IActionResult> Profile()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            
            try
            {
                var userProfile = await service.GetProfileAsync(userId!); //userId can't be null because the user is logged
                return View(userProfile);
            }
            catch (ArgumentNullException ne)
            {
                //TODO error page redirect
                return BadRequest(ne.Message);
            }
        }
    }
}
