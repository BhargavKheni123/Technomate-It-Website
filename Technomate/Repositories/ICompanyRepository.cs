using Technomate.Models;

namespace Technomate.Repositories
{
    public interface ICompanyRepository
    {
        List<CompanyMaster> GetAllCompanies();
    }
}
