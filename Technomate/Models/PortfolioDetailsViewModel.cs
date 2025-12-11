namespace Technomate.Models
{
    public class PortfolioDetailsViewModel
    {
        public Portfolio Portfolio { get; set; }
        public IEnumerable<Portfolio> RecentProjects { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }

}
