// Repositories/ITestimonialRepository.cs
using Technomate.Models;
using System.Collections.Generic;

namespace Technomate.Repositories
{
    public interface ITestimonialRepository
    {
        IEnumerable<Testimonial> GetAll();
        Testimonial GetById(int id);
        void Add(Testimonial testimonial);
        void Update(Testimonial testimonial);
        void Delete(int id);
        IEnumerable<CompanyMaster> GetAllCompanies();

    }
}
