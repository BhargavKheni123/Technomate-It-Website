namespace Technomate.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
