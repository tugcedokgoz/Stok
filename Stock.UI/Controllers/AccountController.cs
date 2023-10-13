using Microsoft.AspNetCore.Mvc;
using Stock.UI.Code;
using Stock.UI.Code.Rest;
using Stock.UI.Models;
using System.Text.Json;

namespace Stock.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();
        //oturum verisini burda döndürüyorum.
        [HttpGet]
        public IActionResult GetToken()
        {
            var token = HttpContext.Session.GetString("token");
            return Json(new { Token = token });
        }

        public IActionResult GirisYap(LoginModel model)
        {

            UserRestClient client = new UserRestClient();
            dynamic result = client.Login(model.UserEmail, model.Password);


            bool success = result.success;

            if (success)
            {
                ViewBag.kullaniciAdi = model.UserEmail;// Kullanıcı adını ViewBag'e eklendi
                Repo.Session.UserEmail = model.UserEmail;
                Repo.Session.Token = (string)result.data;
                Repo.Session.Role = (string)result.role;
                Repo.Session.UserId = (string)result.id;
                Repo.Session.UserFullName = (string)result.userFullName;

                //HttpContext.Session.SetString("UserId", model.UserEmail);
                //HttpContext.Session.SetString("UserEmail", model.UserEmail);
                //HttpContext.Session.SetString("token", (string)result.data); 
                //HttpContext.Session.SetString("role", (string)result.role);

                //giriş yapan epostaya göre kullanıcı bilgilerini burda tutcam 
                //var obje = new { Isim = "Ahmet", Yas = 25 }; //kullanıcı bilgileri olcak apiden dönen değer
                //var jsonStr = JsonSerializer.Serialize(obje);
                //HttpContext.Session.SetString("Kullanici", jsonStr);


                switch ((string)result.role)
                {
                    case "Admin":
                        return RedirectToAction("Home", "Admin");

                    case "Personel":
                        return RedirectToAction("Home", "User");

                    case "Muhasebeci":
                        return RedirectToAction("Home", "Accounter");

                    case "SatınAlma":
                        return RedirectToAction("Home", "Offer");
                    default:
                        // Diğer roller için varsayılan bir yönlendirme, örneğin kullanici sayfasına
                        return RedirectToAction("Home", "Kullanici");
                }

            }
            else
            {

                ViewBag.LoginError = (string)result.message;
                return View("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }

}
