using Microsoft.AspNetCore.Mvc;

namespace Stock.UI.Areas.User.Controllers
{
	[Area("User")]
	//[AuthActionFilter(Role = "Kullanici")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Request() => View();
		public IActionResult SuperiorRequest() => View();
	}
}
