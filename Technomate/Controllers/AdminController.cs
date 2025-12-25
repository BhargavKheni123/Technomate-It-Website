using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using TechnoMate.Repositories;

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

        public IActionResult CompanyMaster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompanyMaster(CompanyMaster model)
        {
            if (model.IsAdmin == model.IsSuperAdmin)
            {
                ModelState.AddModelError("", "Please select either Admin or Super Admin.");
                return View(model);
            }

            if (model.WebsiteImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString()
                                + Path.GetExtension(model.WebsiteImageFile.FileName);

                string folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/WebsiteImages"
                );

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.WebsiteImageFile.CopyTo(stream);
                }

                model.WebsiteImage = "/WebsiteImages/" + fileName;
            }

            _repo.AddCompany(model);

            _repo.AddAdminFromCompany(model);

            return RedirectToAction("CompanyMaster");
        }

    }
}
