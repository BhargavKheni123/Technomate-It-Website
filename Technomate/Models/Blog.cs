using System.ComponentModel.DataAnnotations.Schema;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime PublishedDate { get; set; }
    public string ImageUrl { get; set; }
    public string Category { get; set; }
    public string Tags { get; set; }

    [NotMapped]  // ✅ This tells EF not to look for this column
    public int CommentsCount { get; set; } = 0;

    [NotMapped]
    public string Summary => Content.Length > 200 ? Content.Substring(0, 200) + "..." : Content;
}
