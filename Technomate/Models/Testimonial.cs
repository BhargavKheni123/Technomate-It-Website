using System;
using System.ComponentModel.DataAnnotations;

namespace Technomate.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Message { get; set; }
        public int CompanyId { get; set; }   // ← Add this


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
