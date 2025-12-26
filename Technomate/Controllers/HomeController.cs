using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Technomate.Models;
using Technomate.Repositories;
using Technomate.Repository;

namespace Technomate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        // ✅ SINGLE constructor (CORRECT)
        public HomeController(
            ILogger<HomeController> logger,
            IBlogRepository blogRepo,
            IPortfolioRepository portfolioRepo)
        {
            _logger = logger;
            _blogRepo = blogRepo;
            _portfolioRepo = portfolioRepo;
        }

        public IActionResult Index()
        {
            int companyId;

            if (HttpContext.Session.GetInt32("CompanyId") == null)
            {
                companyId = 1; // Default company
                HttpContext.Session.SetInt32("CompanyId", companyId);
            }
            else
            {
                companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            }

            ViewBag.Blogs = _blogRepo.GetAll(companyId);
            ViewBag.Portfolios = _portfolioRepo.GetAll(companyId);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
