using System.ComponentModel.DataAnnotations;

namespace TechnoMate.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // plain password for now

        public string FullName { get; set; }
    }
}
