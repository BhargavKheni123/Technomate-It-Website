using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;

namespace Technomate.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ITestimonialRepository _testimonialRepo;

        public TestimonialsController(ITestimonialRepository testimonialRepo)
        {
            _testimonialRepo = testimonialRepo;
        }

        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "SuperAdmin")
            {
                // Send all companies to the view for dropdown
                ViewBag.Companies = _testimonialRepo.GetAllCompanies(); // Make a repo method
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                var role = HttpContext.Session.GetString("Role");

                if (role == "Admin")
                {
                    // Admin: save own CompanyId
                    testimonial.CompanyId = Convert.ToInt32(HttpContext.Session.GetString("CompanyId"));
                }
                else if (role == "SuperAdmin")
                {
                    // SuperAdmin: CompanyId already comes from dropdown
                    if (testimonial.CompanyId == 0)
                    {
                        ModelState.AddModelError("CompanyId", "Please select a company");
                        ViewBag.Companies = _testimonialRepo.GetAllCompanies();
                        return View(testimonial);
                    }
                }

                _testimonialRepo.Add(testimonial);
                TempData["Success"] = "Testimonial added!";
                return RedirectToAction("Create");
            }

            // For SuperAdmin, reload companies
            if (HttpContext.Session.GetString("Role") == "SuperAdmin")
            {
                ViewBag.Companies = _testimonialRepo.GetAllCompanies();
            }

            return View(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _testimonialRepo.Add(testimonial);
                TempData["Success"] = "Your testimonial has been submitted!";
            }
            return RedirectToAction("AboutUs2", "AboutUs");
        }

    }

}
