using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;

namespace Technomate.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepo;
        private readonly ISettingRepository _settingRepo;

        public ContactController(IContactRepository contactRepo, ISettingRepository settingRepo)
        {
            _contactRepo = contactRepo;
            _settingRepo = settingRepo;
        }

        public IActionResult Contact()
        {
            var setting = _settingRepo.GetSetting();
            ViewBag.Setting = setting;
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

        public IActionResult Contact2()
        {
            var setting = _settingRepo.GetSetting();
            ViewBag.Setting = setting;
            return View();
        }
    }
}
