using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technomate.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string DetailsUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
