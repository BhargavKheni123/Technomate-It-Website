using Technomate.Models; 
using System.Collections.Generic;

public interface ITestimonialRepository
{
    IEnumerable<Testimonial> GetAllTestimonials();
}
