using System.Collections.Generic;
using System.Linq;
using Technomate.Models;

namespace Technomate.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Portfolio> GetAll()
        {
            return _context.Portfolios.ToList();
        }

        public Portfolio GetById(int id)
        {
            return _context.Portfolios.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
        }

        public void Update(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Portfolios.Find(id);
            if (item != null)
            {
                _context.Portfolios.Remove(item);
                _context.SaveChanges();
            }
        }
        public List<Portfolio> GetByCompany(int companyId)
        {
            return _context.Portfolios
                .Where(p => p.CompanyId == companyId)
                .ToList();
        }

    }
}
