using System.ComponentModel.DataAnnotations.Schema;

namespace Technomate.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
