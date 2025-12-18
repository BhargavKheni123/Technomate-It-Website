using Microsoft.AspNetCore.Mvc;
using TechnoMate.Repositories;
using Microsoft.AspNetCore.Http;

namespace TechnoMate.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _repo;

        public AdminController(IAdminRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser")))
            {
                // Already logged in → redirect to dummy page
                return RedirectToAction("DummyPage");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var admin = _repo.Login(Username, Password);

            if (admin == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }

            // Set session
            HttpContext.Session.SetString("AdminUser", admin.Username);

            return RedirectToAction("Create", "Blog");
        }

        [HttpGet]
        public IActionResult DummyPage()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminUser");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
