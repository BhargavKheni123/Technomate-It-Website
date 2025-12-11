using Microsoft.AspNetCore.Mvc;
using Technomate.Models;

namespace Technomate.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepo;

        public ContactController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveContact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                if (model.CreatedDate == default)
                    model.CreatedDate = DateTime.Now;

                _contactRepo.AddMessage(model);

                TempData["Message"] = "Your message has been sent!";
                return RedirectToAction("Contact"); 
            }

            return View("Contact", model); 
        }

        public IActionResult ContactSuccess()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
