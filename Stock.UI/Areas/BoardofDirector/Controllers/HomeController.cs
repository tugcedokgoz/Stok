using Microsoft.AspNetCore.Mvc;

namespace Stock.UI.Areas.BoardofDirector.Controllers
{
    [Area("BoardofDirector")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Request() => View();

    }
}
