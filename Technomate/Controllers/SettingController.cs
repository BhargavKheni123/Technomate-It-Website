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

        public IActionResult Edit()
        {
            var obj = _settingRepo.GetSetting();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (ModelState.IsValid)
            {
                _settingRepo.UpdateSetting(setting);
                TempData["msg"] = "Setting updated successfully!";
            }
            return View(setting);
        }
    }

}
