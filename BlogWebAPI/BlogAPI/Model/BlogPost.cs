using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.BlogAPI.Model;

public class BlogPost
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Content is required.")]
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters long.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    [MaxLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
    public string Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}



