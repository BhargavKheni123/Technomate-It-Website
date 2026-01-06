using Microsoft.AspNetCore.Mvc;
using Technomate.Repositories;

namespace Technomate.Controllers
{
    public class AboutusController : Controller
    {
        private readonly ISettingRepository _settingRepo;

        public AboutusController(ISettingRepository repo)
        {
            _settingRepo = repo;
        }

        public IActionResult Aboutus()
        {
            var setting = _settingRepo.GetSetting();
            return View(setting);
        }

        public IActionResult Aboutus2()
        {
            return View();
        }
    }

}
