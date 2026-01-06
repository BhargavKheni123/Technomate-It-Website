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
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            if (companyId == 0)
                return RedirectToAction("Login", "Account");

            var data = _repo.GetByCompany(companyId);
            return View(data);
        }

        public IActionResult PortfolioDetails(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
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
            return View();
        }

    }
}
