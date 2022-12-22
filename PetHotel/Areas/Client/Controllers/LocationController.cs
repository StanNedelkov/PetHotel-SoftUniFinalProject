using Microsoft.AspNetCore.Mvc;

namespace PetHotel.Areas.Client.Controllers
{
    [Area("Client")]
    public class LocationController : Controller
    {
        [Route("Map")]
        public IActionResult MAP()
        {
            return View();
        }
    }
}
