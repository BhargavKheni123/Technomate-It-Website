using Microsoft.AspNetCore.Mvc;

namespace Technomate.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult WebDevelopment()
        {
            return View();
        }
        public IActionResult LogoIdentity()
        {
            return View();
        }
        public IActionResult GraphicsDesign()
        {
            return View();
        }

        public IActionResult AppDevelopment()
        {
            return View();
        }

        public IActionResult SocialMarketing()
        {
            return View();
        }
        public ActionResult ContentCreation()
        {
            return View();
        }

    }
}