using Technomate.Models; 
using System.Collections.Generic;
using System.Linq;

public class TestimonialRepository : ITestimonialRepository
{
    private readonly ApplicationDbContext _context;

    public TestimonialRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Testimonial> GetAllTestimonials()
    {
        return _context.Testimonials.OrderByDescending(t => t.CreatedAt).ToList();
    }
}
