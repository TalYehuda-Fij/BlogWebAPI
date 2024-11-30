using BlogWebAPI.BlogAPI.Controllers;
using BlogWebAPI.BlogAPI.DataContext;
using BlogWebAPI.BlogAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebAPI.Tests;

public class BlogPostsControllerTests
{
    private readonly BlogPostsController _controller;
    private readonly BlogDbContext _context;

    public BlogPostsControllerTests()
    {
        _context = TestDbContext.GetInMemoryDbContext();
        _controller = new BlogPostsController(_context);

        _context.BlogPosts.Add(new BlogPost
        {
            Title = "Test Blog Post",
            Content = "This is a test blog post.",
            Author = "Test Author"
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllBlogPosts_ReturnsAllBlogPosts()
    {
        var result = await _controller.GetAllBlogPosts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var blogPosts = Assert.IsAssignableFrom<IEnumerable<BlogPost>>(okResult.Value);

        Assert.Single(blogPosts);
    }

    [Fact]
    public async Task GetBlogPostById_ValidId_ReturnsBlogPost()
    {
        var result = await _controller.GetBlogPostById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var blogPost = Assert.IsType<BlogPost>(okResult.Value);

        Assert.Equal("Test Blog Post", blogPost.Title);
    }

    [Fact]
    public async Task CreateBlogPost_ValidBlogPost_ReturnsCreatedResult()
    {
        var newPost = new BlogPost
        {
            Title = "New Blog Post",
            Content = "This is a new blog post.",
            Author = "New Author"
        };

        var result = await _controller.CreateBlogPost(newPost);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdPost = Assert.IsType<BlogPost>(createdResult.Value);

        Assert.Equal("New Blog Post", createdPost.Title);
    }

    [Fact]
    public async Task UpdateBlogPost_ValidId_UpdatesBlogPost()
    {
        var updatedPost = new BlogPost
        {
            Id = 1,
            Title = "Updated Blog Post",
            Content = "Updated content.",
            Author = "Updated Author"
        };

        var result = await _controller.UpdateBlogPost(1, updatedPost);

        Assert.IsType<NoContentResult>(result);

        var updatedEntity = _context.BlogPosts.FirstOrDefault(bp => bp.Id == 1);
        Assert.NotNull(updatedEntity);
        Assert.Equal("Updated Blog Post", updatedEntity.Title);
    }

    [Fact]
    public async Task DeleteBlogPost_ValidId_DeletesBlogPost()
    {
        var result = await _controller.DeleteBlogPost(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Empty(_context.BlogPosts);
    }



}