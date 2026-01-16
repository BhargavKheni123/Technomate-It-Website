using Microsoft.AspNetCore.Mvc;
using Technomate.Repositories;
using Technomate.Models;

namespace Technomate.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingRepository _settingRepo;

        public FooterViewComponent(ISettingRepository settingRepo)
        {
            _settingRepo = settingRepo;
        }

        public IViewComponentResult Invoke()
        {
            var setting = _settingRepo.GetSetting() ?? new Setting();
            return View(setting);
        }
    }
}
