using Microsoft.AspNetCore.Mvc;
using Stock.UI.Code.Filters;

namespace Stock.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthActionFilter(Role = "Admin")]
    public class HomeController : Controller
    {
       
        
        public IActionResult Index() => View();
        public IActionResult Personel() => View();


    }
}
