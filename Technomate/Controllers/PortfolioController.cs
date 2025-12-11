using Microsoft.AspNetCore.Mvc;

namespace Technomate.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Portfolio()
        {
            return View();
        }

        public IActionResult PortfolioDetails()
        {
            return View();
        }
    }
}
