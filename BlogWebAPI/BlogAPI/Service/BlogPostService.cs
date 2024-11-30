using BlogWebAPI.BlogAPI.DataContext;
using BlogWebAPI.BlogAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.BlogAPI.Service;

public class BlogPostService : IBlogPostService
{
    private readonly BlogDbContext _context;

    public BlogPostService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
    {
        return await _context.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost> GetBlogPostByIdAsync(int id)
    {
        return await _context.BlogPosts.FindAsync(id);
    }

    public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
    {
        _context.BlogPosts.Add(blogPost);
        await _context.SaveChangesAsync();
        return blogPost;
    }

    public async Task UpdateBlogPostAsync(int id, BlogPost blogPost)
    {
        var existing = await _context.BlogPosts.FindAsync(id);
        if (existing != null)
        {
            existing.Title = blogPost.Title;
            existing.Content = blogPost.Content;
            existing.Author = blogPost.Author;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBlogPostAsync(int id)
    {
        var blogPost = await _context.BlogPosts.FindAsync(id);
        if (blogPost != null)
        {
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
        }
    }
}
