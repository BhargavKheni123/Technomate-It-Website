using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;
using Technomate.Repository;

namespace Technomate.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioRepository _repo;
        private readonly ICompanyRepository _companyRepo;

        public PortfolioController(
            IPortfolioRepository repo,
            ICompanyRepository companyRepo)
        {
            _repo = repo;
            _companyRepo = companyRepo;
        }


        public IActionResult Portfolio()
        {
            var data = _repo.GetAllPortfolios();
            return View(data);
        }

        public IActionResult PortfolioDetails(int id)
        {
            var portfolio = _repo.GetById(id);
            if (portfolio == null)
                return NotFound();

            var recentProjects = _repo.GetAllPortfolios()
                                      .Where(p => p.Id != id)
                                      .Take(6)
                                      .ToList();

            var vm = new PortfolioDetailsViewModel
            {
                Portfolio = portfolio,
                RecentProjects = recentProjects,
                Categories = recentProjects.Select(p => p.Category).Distinct()
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            bool isSuperAdmin = HttpContext.Session.GetString("Role") == "SuperAdmin";

            if (isSuperAdmin)
            {
                ViewBag.Companies = _companyRepo.GetAllCompanies();
            }

            ViewBag.IsSuperAdmin = isSuperAdmin;
            return View();
        }


        [HttpPost]
        public IActionResult Create(Portfolio model)
        {
            int sessionCompanyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;

            if (model.CompanyId == 0)
            {
                model.CompanyId = sessionCompanyId;
            }

            if (model.CompanyId == 0)
            {
                ModelState.AddModelError("", "Company not found.");
                ViewBag.Companies = _companyRepo.GetAllCompanies();
                ViewBag.IsSuperAdmin = true;
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/portfolio"
                );

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                model.ImageFile.CopyTo(stream);

                model.ImageUrl = "/uploads/portfolio/" + fileName;
            }

            _repo.Add(model); 
            return RedirectToAction("Portfolio");
        }

        public IActionResult Portfolio2()
        {
            var portfolios = _repo.GetAllPortfolios();

            var categories = portfolios
                .Select(p => p.Category)
                .Distinct()
                .ToList();

            ViewBag.Categories = categories;

            return View(portfolios);
        }

        public IActionResult PortfolioDetails2(int id)
        {
            var portfolio = _repo.GetById(id);
            if (portfolio == null)
                return NotFound();

            var recentProjects = _repo.GetAllPortfolios()
                                      .Where(p => p.Id != id)
                                      .Take(6)
                                      .ToList();

            var vm = new PortfolioDetailsViewModel
            {
                Portfolio = portfolio,
                RecentProjects = recentProjects,
                Categories = recentProjects.Select(p => p.Category).Distinct()
            };

            return View(vm);
        }

    }
}
