using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;

namespace Technomate.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingRepository _settingRepo;

        public SettingController(ISettingRepository settingRepo)
        {
            _settingRepo = settingRepo;
        }

        // GET: Load single record
        public IActionResult Edit()
        {
            var obj = _settingRepo.GetSetting() ?? new Setting();
            return View(obj);
        }

        // POST: Update existing record
        [HttpPost]
        public IActionResult Edit(Setting model)
        {
            if (ModelState.IsValid)
            {
                _settingRepo.UpdateSetting(model);
                TempData["msg"] = "Settings updated successfully!";
                return RedirectToAction("Edit");
            }
            return View(model);
        }
    }
}
