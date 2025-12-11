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


    }
}
