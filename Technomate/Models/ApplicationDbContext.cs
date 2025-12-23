using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechnoMate.Models;

namespace Technomate.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Setting> Setting { get; set; }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<CompanyMaster> CompanyMaster { get; set; }




    }
}
