using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technomate.Models;
using Technomate.Repositories;
using Technomate.Models;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext _context;

    public BlogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetAllBlogsAsync()
    {
        return await _context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .ToListAsync();
    }

    public async Task<Blog> GetBlogBySlugAsync(string slug)
    {
        return await _context.Blogs
            .FirstOrDefaultAsync(b => b.Slug == slug);
    }

    public async Task<List<Blog>> GetRecentBlogsAsync(int count)
    {
        return await _context.Blogs
            .OrderByDescending(b => b.PublishedDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        return await _context.Blogs
            .Select(b => b.Category)
            .Distinct()
            .ToListAsync();
    }
    public async Task AddBlogAsync(Blog blog)
    {
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Blog>> GetBlogsByCompanyAsync(int companyId)
    {
        return await _context.Blogs
            .Where(b => b.CompanyId == companyId)
            .ToListAsync();
    }

}
