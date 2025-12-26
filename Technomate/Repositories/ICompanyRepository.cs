using Technomate.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Technomate.Repositories
{
    public interface ICompanyRepository
    {
        List<CompanyMaster> GetAllCompanies();
    }
}
