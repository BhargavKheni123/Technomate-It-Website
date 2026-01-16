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

        [HttpPost]
        public IActionResult Edit(Setting model)
        {
            if (ModelState.IsValid)
            {
                // Only 1 record exists, update it
                var setting = _settingRepo.GetSetting();
                if (setting != null)
                {
                    setting.Email = model.Email;
                    setting.Phone = model.Phone;
                    setting.Website = model.Website;
                    setting.Address = model.Address;
                    setting.AboutTitle = model.AboutTitle;
                    setting.AboutShortDescription = model.AboutShortDescription;
                    setting.Point1 = model.Point1;
                    setting.Point2 = model.Point2;
                    setting.Point3 = model.Point3;
                    setting.AboutFullDescription = model.AboutFullDescription;
                    setting.Theme = model.Theme; // new theme
                    _settingRepo.UpdateSetting(setting);
                }
                else
                {
                    // No record exists, insert new
                    _settingRepo.AddSetting(model);
                }

                TempData["msg"] = "Settings updated successfully!";
                return RedirectToAction("Edit");
            }
            return View(model);
        }

    }
}
