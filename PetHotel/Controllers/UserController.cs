using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.UserModels;
using PetHotel.Core.Services;

namespace PetHotel.Controllers
{
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
            return View( new RegisterViewModel());
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
                await service.RegisterNewUser(model);
            }
            catch (ArgumentException)
            {
                return View(model);
            }
            //TODO nameof(Login)
            return RedirectToAction("Index", "Home");
        }
    }
}
