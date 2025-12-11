namespace Technomate.Models
{
    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; }
        public List<Blog> RecentPosts { get; set; }
        public List<string> Categories { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
