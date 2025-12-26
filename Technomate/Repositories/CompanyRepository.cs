using Technomate.Models;

namespace Technomate.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CompanyMaster> GetAllCompanies()
        {
            return _context.CompanyMaster
                           .OrderBy(c => c.CompanyName)
                           .ToList();
        }
    }

}
