using BlogWebAPI.BlogAPI.Controllers;
using BlogWebAPI.BlogAPI.DataContext;
using BlogWebAPI.BlogAPI.Model;
using BlogWebAPI.BlogAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace BlogWebAPI.Tests;

public class BlogPostsControllerTests
{
    
    private readonly Mock<IBlogPostService> _mockService;
    private readonly BlogPostsController _controller;

    public BlogPostsControllerTests()
    {
        _mockService = new Mock<IBlogPostService>();
        _controller = new BlogPostsController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllBlogPosts_ReturnsOkResult_WithListOfBlogPosts()
    {
        // Arrange
        var mockBlogPosts = new List<BlogPost>
        {
            new BlogPost { Id = 1, Title = "Test Post 1", Content = "Content 1", Author = "Author 1" },
            new BlogPost { Id = 2, Title = "Test Post 2", Content = "Content 2", Author = "Author 2" }
        };
        _mockService.Setup(service => service.GetAllBlogPostsAsync()).ReturnsAsync(mockBlogPosts);

        // Act
        var result = await _controller.GetAllBlogPosts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBlogPosts = Assert.IsAssignableFrom<IEnumerable<BlogPost>>(okResult.Value);
        Assert.Equal(2, ((List<BlogPost>)returnedBlogPosts).Count);
    }

    [Fact]
    public async Task GetBlogPostById_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var mockBlogPost = new BlogPost { Id = 1, Title = "Test Post", Content = "Content", Author = "Author" };
        _mockService.Setup(service => service.GetBlogPostByIdAsync(1)).ReturnsAsync(mockBlogPost);

        // Act
        var result = await _controller.GetBlogPostById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBlogPost = Assert.IsType<BlogPost>(okResult.Value);
        Assert.Equal("Test Post", returnedBlogPost.Title);
    }

    [Fact]
    public async Task GetBlogPostById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlogPostByIdAsync(99)).ReturnsAsync((BlogPost)null);

        // Act
        var result = await _controller.GetBlogPostById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateBlogPost_ValidBlogPost_ReturnsCreatedResult()
    {
        // Arrange
        var newBlogPost = new BlogPost { Title = "New Post", Content = "Content", Author = "Author" };
        _mockService.Setup(service => service.CreateBlogPostAsync(newBlogPost))
            .ReturnsAsync(new BlogPost { Id = 1, Title = "New Post", Content = "Content", Author = "Author" });

        // Act
        var result = await _controller.CreateBlogPost(newBlogPost);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdBlogPost = Assert.IsType<BlogPost>(createdResult.Value);
        Assert.Equal("New Post", createdBlogPost.Title);
    }

    [Fact]
    public async Task UpdateBlogPost_ValidId_ReturnsNoContent()
    {
        // Arrange
        var updatedBlogPost = new BlogPost { Id = 1, Title = "Updated Post", Content = "Updated Content", Author = "Updated Author" };
        _mockService.Setup(service => service.GetBlogPostByIdAsync(1)).ReturnsAsync(updatedBlogPost);
        _mockService.Setup(service => service.UpdateBlogPostAsync(1, updatedBlogPost)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateBlogPost(1, updatedBlogPost);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateBlogPost_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var updatedBlogPost = new BlogPost { Id = 99, Title = "Updated Post", Content = "Updated Content", Author = "Updated Author" };
        _mockService.Setup(service => service.GetBlogPostByIdAsync(99)).ReturnsAsync((BlogPost)null);

        // Act
        var result = await _controller.UpdateBlogPost(99, updatedBlogPost);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteBlogPost_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var mockBlogPost = new BlogPost { Id = 1, Title = "Test Post", Content = "Content", Author = "Author" };
        _mockService.Setup(service => service.GetBlogPostByIdAsync(1)).ReturnsAsync(mockBlogPost);
        _mockService.Setup(service => service.DeleteBlogPostAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteBlogPost(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBlogPost_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlogPostByIdAsync(99)).ReturnsAsync((BlogPost)null);

        // Act
        var result = await _controller.DeleteBlogPost(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

}

//Not uising Dependency Injection
//private readonly BlogPostsController _controller;
//private readonly BlogDbContext _context;

//public BlogPostsControllerTests()
//{
//    _context = TestDbContext.GetInMemoryDbContext();
//    _controller = new BlogPostsController(_context);

//    _context.BlogPosts.Add(new BlogPost
//    {
//        Title = "Test Blog Post",
//        Content = "This is a test blog post.",
//        Author = "Test Author"
//    });
//    _context.SaveChanges();
//}

//[Fact]
//public async Task GetAllBlogPosts_ReturnsAllBlogPosts()
//{
//    var result = await _controller.GetAllBlogPosts();

//    var okResult = Assert.IsType<OkObjectResult>(result);
//    var blogPosts = Assert.IsAssignableFrom<IEnumerable<BlogPost>>(okResult.Value);

//    Assert.Single(blogPosts);
//}

//[Fact]
//public async Task GetBlogPostById_ValidId_ReturnsBlogPost()
//{
//    var result = await _controller.GetBlogPostById(1);

//    var okResult = Assert.IsType<OkObjectResult>(result);
//    var blogPost = Assert.IsType<BlogPost>(okResult.Value);

//    Assert.Equal("Test Blog Post", blogPost.Title);
//}

//[Fact]
//public async Task CreateBlogPost_ValidBlogPost_ReturnsCreatedResult()
//{
//    var newPost = new BlogPost
//    {
//        Title = "New Blog Post",
//        Content = "This is a new blog post.",
//        Author = "New Author"
//    };

//    var result = await _controller.CreateBlogPost(newPost);

//    var createdResult = Assert.IsType<CreatedAtActionResult>(result);
//    var createdPost = Assert.IsType<BlogPost>(createdResult.Value);

//    Assert.Equal("New Blog Post", createdPost.Title);
//}

//[Fact]
//public async Task UpdateBlogPost_ValidId_UpdatesBlogPost()
//{
//    var updatedPost = new BlogPost
//    {
//        Id = 1,
//        Title = "Updated Blog Post",
//        Content = "Updated content.",
//        Author = "Updated Author"
//    };

//    var result = await _controller.UpdateBlogPost(1, updatedPost);

//    Assert.IsType<NoContentResult>(result);

//    var updatedEntity = _context.BlogPosts.FirstOrDefault(bp => bp.Id == 1);
//    Assert.NotNull(updatedEntity);
//    Assert.Equal("Updated Blog Post", updatedEntity.Title);
//}

//[Fact]
//public async Task DeleteBlogPost_ValidId_DeletesBlogPost()
//{
//    var result = await _controller.DeleteBlogPost(1);

//    Assert.IsType<NoContentResult>(result);
//    Assert.Empty(_context.BlogPosts);
//}
