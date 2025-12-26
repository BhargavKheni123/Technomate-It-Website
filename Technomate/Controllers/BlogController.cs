using Microsoft.AspNetCore.Mvc;
using Technomate.Models;
using Technomate.Repositories;
public class BlogController : Controller
{
    private readonly IBlogRepository _repo;
    private readonly ICompanyRepository _companyRepo;

    public BlogController(
        IBlogRepository repo,
        ICompanyRepository companyRepo)
    {
        _repo = repo;
        _companyRepo = companyRepo;
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

    public IActionResult Create()
    {
        bool isSuperAdmin = HttpContext.Session.GetString("Role") == "SuperAdmin";

        var vm = new BlogCreateViewModel
        {
            Blog = new Blog(),
            IsSuperAdmin = isSuperAdmin
        };

        if (isSuperAdmin)
        {
            vm.Companies = _companyRepo.GetAllCompanies(); // fetch all companies
        }

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogCreateViewModel vm, IFormFile ImageFile)
    {
        // SuperAdmin must select a company
        if (vm.IsSuperAdmin && vm.SelectedCompanyId == 0)
        {
            ModelState.AddModelError("", "Please select a company");
            vm.Companies = _companyRepo.GetAllCompanies();
            return View(vm);
        }

        int companyId = vm.IsSuperAdmin
                        ? vm.SelectedCompanyId
                        : HttpContext.Session.GetInt32("CompanyId") ?? 0;

        if (companyId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        var blog = vm.Blog;
        blog.CompanyId = companyId; // ✅ This ensures correct company
        blog.PublishedDate = DateTime.Now;

        // Image upload logic
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

        await _repo.AddBlogAsync(blog);
        return RedirectToAction("Blog");
    }

}