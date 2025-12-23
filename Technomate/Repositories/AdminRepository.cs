using Technomate.Models;
using TechnoMate.Models;

namespace TechnoMate.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Admin Login(string username, string password)
        {
            return _context.Admin
                .FirstOrDefault(x => x.Username == username && x.PasswordHash == password);
        }
        public void AddCompany(CompanyMaster company)
        {
            _context.CompanyMaster.Add(company);
            _context.SaveChanges();
        }
    }

}
