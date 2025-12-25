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
        int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
        if (companyId == 0)
            return RedirectToAction("Login", "Account");

        int pageSize = 10;

        var allBlogs = await _repo.GetBlogsByCompanyAsync(companyId);

        int totalBlogs = allBlogs.Count;
        int totalPages = (int)Math.Ceiling((double)totalBlogs / pageSize);

        var blogs = allBlogs
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog, IFormFile ImageFile)
    {
        if (ModelState.IsValid)
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            if (companyId == 0)
                return RedirectToAction("Login", "Account");

            blog.CompanyId = companyId; // ✅ IMPORTANT

            // Image upload (same as your code)
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(fileStream);

                blog.ImageUrl = "uploads/" + uniqueFileName;
            }

            blog.PublishedDate = DateTime.Now;

            await _repo.AddBlogAsync(blog);
            return RedirectToAction("Blog");
        }

        return View(blog);
    }

}