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
        private readonly IConfiguration _config; 

        public HomeController(
            ILogger<HomeController> logger,
            IBlogRepository blogRepo,
            IPortfolioRepository portfolioRepo,
            IConfiguration config)
        {
            _logger = logger;
            _blogRepo = blogRepo;
            _portfolioRepo = portfolioRepo;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            int companyId;

            if (HttpContext.Session.GetInt32("CompanyId") == null)
            {
                companyId = _config.GetValue<int>("DefaultCompany:CompanyId");
                HttpContext.Session.SetInt32("CompanyId", companyId);
            }
            else
            {
                companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            }

            ViewBag.Blogs = await _blogRepo.GetBlogsByCompanyAsync(companyId);
            ViewBag.Portfolios = _portfolioRepo.GetByCompany(companyId);

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
        public IActionResult Index2()
        {
            return View();
        }
    }
}
