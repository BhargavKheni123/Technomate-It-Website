using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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


    }
}
