using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repository;

namespace Technomate.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioRepository _repo;

        public PortfolioController(IPortfolioRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Portfolio()
        {
            var data = _repo.GetAll();
            return View(data);
        }

        public IActionResult PortfolioDetails(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ADMIN CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // ADMIN CREATE - POST
        [HttpPost]
        public IActionResult Create(Portfolio model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/portfolio");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                model.ImageUrl = "/uploads/portfolio/" + fileName;
            }

            _repo.Add(model);
            return RedirectToAction("Portfolio");
        }


    }
}
