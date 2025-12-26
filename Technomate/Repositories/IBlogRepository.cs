using Technomate.Models;
using System.Collections.Generic;

namespace Technomate.Repositories
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogBySlugAsync(string slug);
        Task<List<Blog>> GetRecentBlogsAsync(int count);
        Task<List<string>> GetCategoriesAsync();
        Task AddBlogAsync(Blog blog);
        Task<List<Blog>> GetBlogsByCompanyAsync(int companyId);
        List<Blog> GetAll(int companyId);

    }

}
