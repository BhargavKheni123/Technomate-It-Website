// Repositories/TestimonialRepository.cs
using Technomate.Models;
using System.Collections.Generic;
using System.Linq;

namespace Technomate.Repositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly ApplicationDbContext _context;

        public TestimonialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Testimonial> GetAll()
        {
            return _context.Testimonials.OrderByDescending(t => t.CreatedAt).ToList();
        }

        public Testimonial GetById(int id)
        {
            return _context.Testimonials.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
        }

        public void Update(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var t = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (t != null)
            {
                _context.Testimonials.Remove(t);
                _context.SaveChanges();
            }
        }
        public IEnumerable<CompanyMaster> GetAllCompanies()
        {
            return _context.CompanyMaster.ToList();
        }

    }
}
