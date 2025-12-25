using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technomate.Models   // ✅ This must match exactly
{
    public class Blog
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        public string Tags { get; set; }

        [NotMapped]
        public int CommentsCount { get; set; } = 0;

        [NotMapped]
        public string Summary => Content.Length > 200 ? Content.Substring(0, 200) + "..." : Content;
    }
}
