using System.Collections.Generic;

namespace Technomate.Models
{
    public class BlogDetailsViewModel
    {
        public Blog Blog { get; set; }
        public List<Blog> RecentPosts { get; set; }
        public List<string> Categories { get; set; }
    }
}
