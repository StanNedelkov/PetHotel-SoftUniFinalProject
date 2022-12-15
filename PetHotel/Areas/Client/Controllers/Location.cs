using Microsoft.AspNetCore.Mvc;

namespace PetHotel.Areas.Client.Controllers
{
    [Area("Client")]
    public class Location : Controller
    {
        [Route("Map")]
        public IActionResult Map()
        {
            return View();
        }
    }
}
