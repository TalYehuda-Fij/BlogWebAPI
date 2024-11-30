using BlogWebAPI.BlogAPI.Model;

namespace BlogWebAPI.BlogAPI.Service;

public interface IBlogPostService
{
    Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
    Task<BlogPost> GetBlogPostByIdAsync(int id);
    Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost);
    Task UpdateBlogPostAsync(int id, BlogPost blogPost);
    Task DeleteBlogPostAsync(int id);
}
