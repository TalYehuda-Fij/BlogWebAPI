using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.BlogAPI.Model;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinLength(10)]
    public string Content { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    [MaxLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
    public string Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}



