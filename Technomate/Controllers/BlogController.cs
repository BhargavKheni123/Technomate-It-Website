using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;
public class BlogController : Controller
{
    private readonly IBlogRepository _repo;

    public BlogController(IBlogRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Blog(int page = 1)
    {
        int pageSize = 10;

        var allBlogs = await _repo.GetAllBlogsAsync();
        int totalBlogs = allBlogs.Count;

        int totalPages = (int)Math.Ceiling((double)totalBlogs / pageSize);

        var blogs = allBlogs
            .OrderByDescending(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var recentPosts = await _repo.GetRecentBlogsAsync(5);
        var categories = await _repo.GetCategoriesAsync();

        var model = new BlogViewModel
        {
            Blogs = blogs,
            RecentPosts = recentPosts,
            Categories = categories,

            CurrentPage = page,
            TotalPages = totalPages
        };

        return View(model);
    }


    public async Task<IActionResult> BlogDetails(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            return NotFound();

        var blog = await _repo.GetBlogBySlugAsync(slug);
        if (blog == null)
            return NotFound();

        // Get recent posts and categories for sidebar
        var recentPosts = await _repo.GetRecentBlogsAsync(5);
        var categories = await _repo.GetCategoriesAsync();

        var detailsModel = new BlogDetailsViewModel
        {
            Blog = blog,
            RecentPosts = recentPosts,
            Categories = categories
        };

        return View(detailsModel);
    }

    // GET: /Blog/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Blog/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog, IFormFile ImageFile)
    {
        if (ModelState.IsValid)
        {
            // Handle image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Save relative path in database
                blog.ImageUrl = "uploads/" + uniqueFileName;
            }

            blog.PublishedDate = DateTime.Now;
            await _repo.AddBlogAsync(blog);

            return RedirectToAction("Blog"); // redirect to blog list
        }

        return View(blog);
    }

}