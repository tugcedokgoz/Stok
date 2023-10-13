using Microsoft.AspNetCore.Mvc;

namespace Stock.UI.Areas.Offer.Controllers
{
    [Area("Offer")]
    //[AuthActionFilter(Role = "Kullanici")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Offer() => View();
        public IActionResult Request() => View();
        public IActionResult Stock() => View();
        public IActionResult Product() => View();

    }
}
