using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;

namespace Technomate.Controllers
{
    public class AboutusController : Controller
    {
        private readonly ISettingRepository _settingRepo;
        private readonly ITestimonialRepository _testimonialRepo;

        public AboutusController(ISettingRepository settingRepo, ITestimonialRepository testimonialRepo)
        {
            _settingRepo = settingRepo;
            _testimonialRepo = testimonialRepo;
        }

        public IActionResult Aboutus()
        {
            var setting = _settingRepo.GetSetting();

            var testimonials = _testimonialRepo.GetAll();

            ViewBag.Testimonials = testimonials ?? new List<Testimonial>();

            return View(setting);
        }

        public IActionResult Aboutus2()
        {
            var setting = _settingRepo.GetSetting();

            var testimonials = _testimonialRepo.GetAll();

            ViewBag.Testimonials = testimonials ?? new List<Testimonial>();

            return View(setting);
        }

    }

}
