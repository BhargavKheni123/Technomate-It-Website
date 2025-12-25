using Technomate.Models;
using TechnoMate.Models;

namespace TechnoMate.Repositories
{
    public interface IAdminRepository
    {
        Admin Login(string username, string password);
        void AddCompany(CompanyMaster company);
        void AddAdminFromCompany(CompanyMaster company);
        CompanyMaster GetCompanyById(int companyId);


    }


}
