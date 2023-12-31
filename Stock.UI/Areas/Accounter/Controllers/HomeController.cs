﻿using Microsoft.AspNetCore.Mvc;

namespace Stock.UI.Areas.Accounter.Controllers
{
    [Area("Accounter")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approved() => View();
        public IActionResult Stock() => View();
        public IActionResult Bill() => View();
    }
}
