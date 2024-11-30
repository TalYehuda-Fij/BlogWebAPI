using BlogWebAPI.BlogAPI.DataContext;
using BlogWebAPI.BlogAPI.Model;
using BlogWebAPI.BlogAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.BlogAPI.Controllers;

[ApiController]
[Route("/blogposts")]
public class BlogPostsController : ControllerBase
{
    private readonly IBlogPostService _service;

    public BlogPostsController(IBlogPostService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts()
    {
        var blogPosts = await _service.GetAllBlogPostsAsync();
        return Ok(blogPosts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogPostById(int id)
    {
        var blogPost = await _service.GetBlogPostByIdAsync(id);

        if (blogPost == null)
            return NotFound();

        return Ok(blogPost);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlogPost([FromBody] BlogPost blogPost)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdBlogPost = await _service.CreateBlogPostAsync(blogPost);

        return CreatedAtAction(nameof(GetBlogPostById), new { id = createdBlogPost.Id }, createdBlogPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] BlogPost updatedBlogPost)
    {
        if (id != updatedBlogPost.Id)
            return BadRequest("ID mismatch.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var blogPost = await _service.GetBlogPostByIdAsync(id);

        if (blogPost == null)
            return NotFound();

        await _service.UpdateBlogPostAsync(id, updatedBlogPost);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        var blogPost = await _service.GetBlogPostByIdAsync(id);

        if (blogPost == null)
            return NotFound();

        await _service.DeleteBlogPostAsync(id);

        return NoContent();
    }


}


//[HttpGet]
//public async Task<IActionResult> GetAllBlogPosts(int pageNumber = 1, int pageSize = 25)
//{
//    var blogPosts = await _context.BlogPosts
//        .Skip((pageNumber - 1) * pageSize)
//        .Take(pageSize)
//        .ToListAsync();

//    return Ok(blogPosts);
//}


//[HttpPost]
//public async Task<IActionResult> CreateBlogPost([FromBody] BlogPost blogPost)
//{
//    if (!ModelState.IsValid)
//        return BadRequest(ModelState);


//    _context.BlogPosts.Add(blogPost);
//    await _context.SaveChangesAsync();

//    return CreatedAtAction(nameof(GetBlogPostById), new { id = blogPost.Id }, blogPost);
//}

//[HttpGet("{id}")]
//public async Task<IActionResult> GetBlogPostById(int id)
//{
//    var blogPost = await _context.BlogPosts.FindAsync(id);

//    if (blogPost == null)
//        return NotFound();

//    return Ok(blogPost);
//}

//[HttpPut("{id}")]
//public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] BlogPost updatedBlogPost)
//{
//    if (id != updatedBlogPost.Id)
//        return BadRequest("ID mismatch.");

//    if (!ModelState.IsValid)
//        return BadRequest(ModelState);

//    var existingBlogPost = await _context.BlogPosts.FindAsync(id);

//    if (existingBlogPost == null)
//        return NotFound();

//    if (_context.BlogPosts.Any(bp => bp.Title == updatedBlogPost.Title && bp.Id != id))
//        return Conflict("A blog post with the same title already exists.");

//    existingBlogPost.Title = updatedBlogPost.Title;
//    existingBlogPost.Content = updatedBlogPost.Content;
//    existingBlogPost.Author = updatedBlogPost.Author;

//    await _context.SaveChangesAsync();

//    return NoContent();
//}

//[HttpDelete("{id}")]
//public async Task<IActionResult> DeleteBlogPost(int id)
//{
//    var blogPost = await _context.BlogPosts.FindAsync(id);

//    if (blogPost == null)
//        return NotFound();

//    _context.BlogPosts.Remove(blogPost);
//    await _context.SaveChangesAsync();

//    return NoContent();
//}

