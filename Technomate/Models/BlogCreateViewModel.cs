namespace Technomate.Models
{
    public class BlogCreateViewModel
    {
        public Blog Blog { get; set; }
        public List<CompanyMaster> Companies { get; set; }
        public int SelectedCompanyId { get; set; }
        public bool IsSuperAdmin { get; set; }

    }

}
