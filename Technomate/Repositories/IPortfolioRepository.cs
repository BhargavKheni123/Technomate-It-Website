using System.Collections.Generic;
using Technomate.Models;

namespace Technomate.Repository
{
    public interface IPortfolioRepository
    {
        IEnumerable<Portfolio> GetAll();
        Portfolio GetById(int id);
        void Add(Portfolio portfolio);
        void Update(Portfolio portfolio);
        void Delete(int id);
        List<Portfolio> GetByCompany(int companyId);
        List<Blog> GetAll(int companyId);

    }
}
