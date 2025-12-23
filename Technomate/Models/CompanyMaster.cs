using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technomate.Models
{
    public class CompanyMaster
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public bool IsAdmin { get; set; }
        public string WebsiteUrl { get; set; }
        public string WebsiteName { get; set; }

        // DB me path save hoga
        public string WebsiteImage { get; set; }

        // FILE receive karne ke liye (DB me save nahi hota)
        [NotMapped]
        public IFormFile WebsiteImageFile { get; set; }

        public bool IsSuperAdmin { get; set; }
    }

}

