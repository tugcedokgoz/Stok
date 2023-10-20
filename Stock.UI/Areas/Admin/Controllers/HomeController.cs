using Microsoft.AspNetCore.Mvc;
using Stock.UI.Code.Filters;

namespace Stock.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthActionFilter(Role = "Admin")]
    public class HomeController : Controller
    {
       
        
        public IActionResult Index() => View();
        public IActionResult Personel() => View();
        public IActionResult Role() => View();
        public IActionResult Company() => View();
        public IActionResult Department() => View();
        public IActionResult Request() => View();


    }
}
