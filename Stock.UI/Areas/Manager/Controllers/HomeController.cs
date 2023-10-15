using Microsoft.AspNetCore.Mvc;

namespace Stock.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Request() => View();
    }
}
